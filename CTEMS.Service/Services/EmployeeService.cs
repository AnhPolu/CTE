using AutoMapper;
using CTEMS.Infrastructure;
using CTEMS.Lib.Model;
using CTEMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace CTEMS.Service.Services
{
    public interface IEmployeeService
    {
        List<EmployeeVM> GetAll();
        Task<EmployeeVM> GetById(int id);
        Task<EmployeeVM> Add(EmployeeVM employee);
        Task<EmployeeVM> Update(EmployeeVM employee);
        Task<bool> Delete(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Employee> _employeeRepository;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        static Regex ConvertToUnsign_rg = null;
        public EmployeeService(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _employeeRepository = unitOfWork.Repository<Employee>();
            _mapper = mapper;
            _userService = userService;
        }

        public List<EmployeeVM> GetAll()
        {
            try
            {
                IQueryable employees = _employeeRepository.GetAll();
                return _mapper.Map<List<EmployeeVM>>(employees);
            }
            catch(Exception)
            {
                throw;
            } 
        }

        public async Task<EmployeeVM> GetById(int id)
        {
            try
            {
                Employee employee = await _employeeRepository.GetByIdAsync(id);
                return _mapper.Map<EmployeeVM>(employee);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<EmployeeVM> Add(EmployeeVM employee)
        {
            try
            {
                if (await _employeeRepository.ExistAsync(x => x.Email == employee.Email && x.IdentityNumber == employee.IdentityNumber))
                {
                    Employee employeeEntity = _mapper.Map<Employee>(employee);
                    await _employeeRepository.AddAsync(employeeEntity);

                    string uniqueUserName = await GenerateUniqueUserName(employee.FirstName, employee.LastName);
                    UserVM userVM = new UserVM();
                    userVM.Email = employee.Email;
                    userVM.UserName = uniqueUserName;
                    userVM.RequiredToChangePass = true;
                    userVM.EmployeeId = employeeEntity.Id;
                    userVM.Salt = 1234;
                    userVM.Password = "12345678x@X";
                    userVM.DefaultResetPassword = "12345678x@X";
                    await _userService.Add(userVM);

                    await _unitOfWork.CommitAsync();

                    return _mapper.Map<EmployeeVM>(employeeEntity);
                }
                else
                {
                    await _unitOfWork.RollbackAsync();
                    throw new Exception("Employee already exist"); 
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<EmployeeVM> Update(EmployeeVM employee)
        {
            try
            {
                if (await _employeeRepository.ExistAsync(x => x.Id == employee.Id))
                {
                    Employee employeeEntity = _mapper.Map<Employee>(employee);
                    await _employeeRepository.Update(employeeEntity);

                    return _mapper.Map<EmployeeVM>(employeeEntity);
                } 
                else
                {
                    throw new KeyNotFoundException(employee.Id.ToString());
                }
            }
            catch (Exception)
            {
                throw;
            }
        }

        public async Task<bool> Delete(int employeeId)
        {
            try
            {
                Employee employee = await _employeeRepository.GetByIdAsync(employeeId);
                if (employee != null)
                {
                    await _employeeRepository.Delete(employee);
                    await _unitOfWork.CommitAsync();
                    return true;
                }
                else
                {
                    await _unitOfWork.RollbackAsync();
                    throw new Exception("Employee not found");
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<string> GenerateUniqueUserName(string firstName, string lastName)
        {
            firstName = firstName.Trim().ToLowerInvariant().Replace(" ", string.Empty);
            lastName = lastName.Trim().ToLowerInvariant().Replace(" ", string.Empty);

            firstName = ConvertToUnsign(firstName);
            lastName = ConvertToUnsign(lastName);

            string uniqueUserName = firstName;

            int counter = 1;
            while (await _userService.ExistAsync(x => x.UserName == uniqueUserName))
            {
                uniqueUserName = $"{firstName}{lastName}{counter}";
                counter++;
            }

            return uniqueUserName;
        }

        public static string ConvertToUnsign(string strInput)
        {
            if (ReferenceEquals(ConvertToUnsign_rg, null))
            {
                ConvertToUnsign_rg = new Regex("p{IsCombiningDiacriticalMarks}+");
            }
            var temp = strInput.Normalize(NormalizationForm.FormD);
            return ConvertToUnsign_rg.Replace(temp, string.Empty).Replace("đ", "d").Replace("Đ", "D").ToLower();
        }
    }
}
