using AutoMapper;
using CTEMS.Infrastructure;
using CTEMS.Lib.Model;
using CTEMS.Service.DTO;
using Microsoft.EntityFrameworkCore;
using NuGet.DependencyResolver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
namespace CTEMS.Service.Services
{
    public interface ITeacherServive
    {
        Task<List<TeacherVM>> GetAll();
        Task<TeacherVM> GetById(int id);
        Task<TeacherVM> Add(TeacherVM teacher);
        Task<TeacherVM> Update(TeacherVM teacher);
        Task Delete(int id);
    }
    public class TeacherServive : ITeacherServive
    {

        private readonly IUnitOfWork _unitOfWork;
        private readonly IGenericRepository<Teacher> _teacherRepository = null;
        private readonly IUserService _userService;
        private readonly IMapper _mapper;
        public TeacherServive(IUnitOfWork unitOfWork, IMapper mapper, IUserService userService)
        {
            _unitOfWork = unitOfWork;
            _teacherRepository = unitOfWork.Repository<Teacher>();
            _mapper = mapper;
            _userService = userService;
        }


        /// <summary>
        /// Adds a new teacher to the system.
        /// </summary>
        /// <param name="teacher">The teacher to be added.</param>
        /// <returns>A task representing the operation, containing the added teacher.</returns>
        public async Task<TeacherVM> Add(TeacherVM teacher)
        {
            try
            {
                // Map the TeacherVM to the Teacher entity
                Teacher teacherEntity = _mapper.Map<Teacher>(teacher);

                // Add the teacher to the repository
                await _teacherRepository.AddAsync(teacherEntity);

                // Commit the changes
                await _unitOfWork.CommitAsync();

                // Map the Teacher entity back to TeacherVM and return

                return _mapper.Map<TeacherVM>(teacherEntity);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }

        /// <summary>
        /// Deletes a teacher from the system by their ID.
        /// </summary>
        /// <param name="id">The ID of the teacher to be deleted.</param>
        public async Task Delete(int id)
        {
            try
            {
                Teacher teacher = await _teacherRepository.GetByIdAsync(id);
                if (teacher != null)
                {
                    await _teacherRepository.Delete(teacher);
                    await _unitOfWork.CommitAsync();
                }
                else
                {
                    throw new KeyNotFoundException(id.ToString());
                }
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
        
        

        /// <summary>
        /// Retrieves a list of all teachers in the system.
        /// </summary>
        /// <returns>A task representing the operation, containing a list of TeacherVM objects.</returns>
        public async Task<List<TeacherVM>> GetAll()
        {
            // Assuming you have a DbContext or a repository to access the data
            var teachers = await _teacherRepository.GetAllAsync();
            var teacherVMs = _mapper.Map<List<TeacherVM>>(teachers);
            return teacherVMs;
        }

        /// <summary>
        /// Retrieves a teacher by their unique identifier.
        /// </summary>
        /// <param name="id">The ID of the teacher to be retrieved.</param>
        /// <returns>A task representing the operation, containing the retrieved teacher.</returns>
        public async Task<TeacherVM> GetById(int id)
        {
            try
            {
                Teacher teacher = await _teacherRepository.GetByIdAsync(id);
                return _mapper.Map<TeacherVM>(teacher);
            }
            catch (Exception)
            {
                throw;
            }
        }

        /// <summary>
        /// Updates an existing teacher in the system.
        /// </summary>
        /// <param name="teacher">The teacher to be updated.</param>
        /// <returns>The updated teacher.</returns>
        public async Task<TeacherVM> Update(TeacherVM teacher)
        {
            try
            {
                // Check if the teacher exists
                if (!await _teacherRepository.ExistAsync(x => x.Id == teacher.Id))
                {
                    throw new KeyNotFoundException(teacher.Id.ToString());
                }

                // Map the TeacherVM to the Teacher entity
                Teacher teacherEntity = _mapper.Map<Teacher>(teacher);

                // Update the teacher in the repository
                await _teacherRepository.Update(teacherEntity);

                // Commit the changes
                await _unitOfWork.CommitAsync();

                // Map the Teacher entity back to TeacherVM and return
                return _mapper.Map<TeacherVM>(teacherEntity);
            }
            catch (Exception)
            {
                await _unitOfWork.RollbackAsync();
                throw;
            }
        }
    }
}
