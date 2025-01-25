using CTEMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Service.Services
{
    public interface IStudentService
    {
        List<EmployeeVM> GetAll();
        EmployeeVM GetById(int id);
        Task<EmployeeVM> Add(EmployeeVM employee);
        EmployeeVM Update(EmployeeVM employee);
        void Delete(int id);
    }
    public class StudentService
    {
    }
}
