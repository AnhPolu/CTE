using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTEMS.Service.DTO;

namespace CTEMS.Service.Services;
public interface ICourseServive
{
    Task<List<CourseVM>> GetAll();
    Task<CourseVM> GetById(int id);
    Task<CourseVM> Add(CourseVM teacher);
    Task<CourseVM> Update(CourseVM teacher);
    Task Delete(int id);
}
    
public class CourseService
{

}
