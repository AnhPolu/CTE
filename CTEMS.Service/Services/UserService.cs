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
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Service.Services
{
    public interface IUserService
    {
        UserVM Add(UserVM user);
        UserVM Update(UserVM user);
        void Delete(int id);
        AuthenticateResponse Authenticate(AuthenticateRequest model);
        User GetById(int id);
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

        public UserVM Add(UserVM user)
        {
            try
            {
                if (!this._userRepository.Exist(x => x.UserName == user.UserName))
                {
                    User userEntity = _mapper.Map<User>(user);
                    this._userRepository.Add(userEntity);
                    this._unitOfWork.Commit();

                    return _mapper.Map<UserVM>(userEntity);
                }
                else
                {
                    throw new Exception("User already exist");
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public UserVM Update(UserVM user)
        {
            try
            {
                if (this._userRepository.Exist(x => x.Id == user.Id))
                {
                    User userEntity = _mapper.Map<User>(user);
                    this._userRepository.Update(userEntity);
                    this._unitOfWork.Commit();

                    return _mapper.Map<UserVM>(userEntity);
                }
                else
                {
                    throw new KeyNotFoundException(user.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }




        public void Delete(int id)
        {
            try
            {
                User user = this._userRepository.GetById(id);
                if (user != null)
                {
                    this._userRepository.Delete(user);
                    this._unitOfWork.Commit();
                }
                else
                {
                    throw new KeyNotFoundException(user.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
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
