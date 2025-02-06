using AutoMapper;
using CTEMS.Infrastructure;
using CTEMS.Lib.Model;
using CTEMS.Service.DTO;
using CTEMS.Service.Helpers;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Service.Services
{
    public interface IUserService
    {
        Task<UserVM> Add(UserVM user);
        Task<UserVM> Update(UserVM user);
        void Delete(int id);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
        Task<bool> ExistAsync(Expression<Func<User, bool>> expression);
    }

    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly JwtConfig _jwtConfig;
        private readonly IGenericRepository<User> _userRepository;
        private readonly IMapper _mapper;

        public UserService(IOptions<JwtConfig> jwtConfig, IUnitOfWork unitOfWork, IMapper mapper)
        {
            _jwtConfig = jwtConfig.Value;
            _unitOfWork = unitOfWork;
            _mapper = mapper;
            _userRepository = _unitOfWork.Repository<User>();
        }

        public User GetById(int id)
        {
            return _userRepository.GetById(id);
        }
        public async Task<UserVM> Add(UserVM user)
        {
            try
            {
                if (!(await _userRepository.ExistAsync(x => x.UserName == user.UserName)))
                {
                    User userEntity = _mapper.Map<User>(user);
                    userEntity.Password = MD5Hash(user.Password);
                    await _userRepository.AddAsync(userEntity);
                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<UserVM>(userEntity);
                }
                else
                {
                    await _unitOfWork.RollbackAsync();
                    throw new Exception("User already exist");
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        public async Task<UserVM> Update(UserVM user)
        {
            if (!await _userRepository.ExistAsync(x => x.Id == user.Id))
            {
                throw new KeyNotFoundException(user.Id.ToString());
            }

            var userEntity = _mapper.Map<User>(user);
            await _userRepository.Update(userEntity);
            await _unitOfWork.CommitAsync();

            return _mapper.Map<UserVM>(userEntity);
        }
        public void Delete(int id)
        {
            try
            {
                User user = _userRepository.GetById(id);
                if (user != null)
                {
                    _userRepository.Delete(user);
                    _unitOfWork.Commit();
                }
                else
                {
                    throw new KeyNotFoundException(user.Id.ToString());
                }
            }
            catch (Exception)
            {
                _unitOfWork.Rollback();
                throw;
            }
        }

        public async Task<bool> ExistAsync(Expression<Func<User, bool>> expression)
        {
            return await _userRepository.ExistAsync(expression);
        }

        public static string MD5Hash(string input)
        {
            StringBuilder hash = new StringBuilder();
            MD5CryptoServiceProvider md5provider = new MD5CryptoServiceProvider();
            byte[] bytes = md5provider.ComputeHash(new UTF8Encoding().GetBytes(input));

            for (int i = 0; i < bytes.Length; i++)
            {
                hash.Append(bytes[i].ToString("x2"));
            }
            return hash.ToString();
        }

        public AuthenticateResponse Authenticate(AuthenticateRequest model)
        {
            string hassedPassword = MD5Hash(model.Password);
            var user = _userRepository.GetSingleBy(x => x.UserName == model.Username && x.Password == hassedPassword);
            // return null if user not found
            if (user == null) return null;
            // authentication successful so generate jwt token
            var token = generateJwtToken(user);
            return new AuthenticateResponse(user, token);
        }

        private string generateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_jwtConfig.Secret);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[] { new Claim("id", user.Id.ToString()) }),
                Expires = DateTime.UtcNow.AddDays(3),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }


    }
}
