using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTEMS.Service.DTO;

namespace CTEMS.Service.Services;

public interface ICandidateServive
{
    Task<List<TeacherVM>> GetAll();
    Task<TeacherVM> GetById(int id);
    Task<TeacherVM> Add(TeacherVM teacher);
    Task<TeacherVM> Update(TeacherVM teacher);
    Task Delete(int id);
}
public class CandidateService
{

}
