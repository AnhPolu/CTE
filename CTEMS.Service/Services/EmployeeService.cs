using AutoMapper;
using CTEMS.Infrastructure;
using CTEMS.Lib.Model;
using CTEMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Service.Services
{
    public interface IEmployeeService
    {
        List<EmployeeVM> GetAll();
        EmployeeVM GetById(int id);
        Task<EmployeeVM> Add(EmployeeVM employee);
        EmployeeVM Update(EmployeeVM employee);
        void Delete(int id);
    }
    public class EmployeeService : IEmployeeService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IGenericRepository<Employee> employeeRepository;
        private readonly IMapper mapper;
        public EmployeeService(IUnitOfWork _unitOfWork, IMapper _mapper)
        {
            unitOfWork = _unitOfWork;
            employeeRepository = unitOfWork.Repository<Employee>();
            mapper = _mapper;
        }

        public List<EmployeeVM> GetAll()
        {
            try
            {
                IQueryable employees = employeeRepository.GetAll();
                return mapper.Map<List<EmployeeVM>>(employees);
            }
            catch(Exception ex)
            {
                throw ex;
            } 
        }

        public EmployeeVM GetById(int id)
        {
            try
            {
                Employee employee = employeeRepository.GetById(id);
                return mapper.Map<EmployeeVM>(employee);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task<EmployeeVM> Add(EmployeeVM employee)
        {
            try
            {
                if (await this.employeeRepository.ExistAsync(x => x.Email == employee.Email && x.IdentityNumber == employee.IdentityNumber))
                {
                    Employee employeeEntity = mapper.Map<Employee>(employee);
                    await this.employeeRepository.AddAsync(employeeEntity);
                    await unitOfWork.CommitAsync();

                    return mapper.Map<EmployeeVM>(employeeEntity);
                }
                else
                {
                    throw new Exception("Employee already exist"); 
                }
            }
            catch (Exception ex)
            {
                await unitOfWork.RollbackAsync();
                throw;
            }
        }

        public async Task<EmployeeVM> Update(EmployeeVM employee)
        {
            try
            {
                if (await this.employeeRepository.ExistAsync(x => x.Id == employee.Id))
                {
                    Employee employeeEntity = mapper.Map<Employee>(employee);
                    this.employeeRepository.Update(employeeEntity);

                    return mapper.Map<EmployeeVM>(employeeEntity);
                } 
                else
                {
                    throw new KeyNotFoundException(employee.Id.ToString());
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
                Employee employee = this.employeeRepository.GetById(id);
                if (employee != null)
                {
                    this.employeeRepository.Delete(employee);
                }
                else
                {
                    throw new KeyNotFoundException(employee.Id.ToString());
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        EmployeeVM IEmployeeService.Update(EmployeeVM employee)
        {
            throw new NotImplementedException();
        }
    }
}
