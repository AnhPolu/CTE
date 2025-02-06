using CTEMS.Lib.Model;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CTEMS.Lib.Context
{
    public interface IApplicationDbContext : IDisposable
    {
        public  DbSet<Candidate> Candidates { get; set; }
        public  DbSet<Course> Courses { get; set; }
        public  DbSet<Employee> Employees { get; set; }
        public  DbSet<FinalTest> FinalTests { get; set; }
        public  DbSet<Lesson> Lessons { get; set; }
        public  DbSet<LessonStudent> LessonStudents { get; set; }
        public  DbSet<Level> Levels { get; set; }
        public  DbSet<PlacementTest> PlacementTests { get; set; }
        public DbSet<Student> CourseCandidates { get; set; }
        public  DbSet<Teacher> Teachers { get; set; }
        public int Save();
        public Task<int> SaveChangesAsync();
    }
}
