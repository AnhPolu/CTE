using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using CTEMS.Service.DTO;

namespace CTEMS.Service.Services;

public interface ILevelServive
    {
        Task<List<LevelVM>> GetAll();
        Task<LevelVM> GetById(int id);
        Task<LevelVM> Add(LevelVM level);
        Task<LevelVM> Update(LevelVM level);
        Task Delete(int id);
    }
public class LevelService
{

}
