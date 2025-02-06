using CTEMS.Service.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Service.Services;

public interface IStudentService
{
    Task<List<StudentVM>> GetAll();
    Task<StudentVM> GetById(int id);
    Task<StudentVM> Add(StudentVM teacher);
    Task<StudentVM> Update(StudentVM teacher);
    Task Delete(int id);
}
public class StudentService 
{

}

