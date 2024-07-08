using System;
using System.Reflection.Emit;
using System.Threading.Tasks;
using CTEMS.Lib.Model;
using Microsoft.EntityFrameworkCore;

namespace CTEMS.Lib.Context
{
    public class ApplicationDbContext : DbContext, IApplicationDbContext
    {
        public ApplicationDbContext()
        {
        }
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) {
        }

        public DbSet<Candidate> Candidates { get; set; }
        public DbSet<Course> Courses { get; set; }
        public DbSet<CourseTeacher> CourseTeachers { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<FinalTest> FinalTests { get; set; }
        public DbSet<Lesson> Lessons { get; set; }
        public DbSet<LessonStudent> LessonStudents { get; set; }
        public DbSet<Level> Levels { get; set; }
        public DbSet<PlacementTest> PlacementTests { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<CourseCandidate> CourseCandidates { get; set; }
        public DbSet<Teacher> Teachers { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
        public int Save()
        {
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=EMCTEDB;Integrated Security=True");
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            // Candidate 
            builder.Entity<Candidate>().HasKey(c => c.Id);

            builder.Entity<Candidate>()
            .HasMany(c => c.CourseCandidates)
            .WithOne(s => s.Candidate)
            .HasForeignKey(s => s.CandidateId);

            builder.Entity<Candidate>()
            .HasMany(c => c.PlacementTests)
            .WithOne(s => s.Candidate)
            .HasForeignKey(s => s.CandidateId);

            // Course
            builder.Entity<Course>().HasKey(c => c.Id);
            builder.Entity<Course>()
           .HasMany(c => c.CourseCandidates)
           .WithOne(s => s.Course)
           .HasForeignKey(s => s.CourseId);

            builder.Entity<Course>()
           .HasMany(c => c.Lessons)
           .WithOne(s => s.Course)
           .HasForeignKey(s => s.CourseId);

            builder.Entity<Course>()
            .HasMany(c => c.CourseTeachers)
            .WithOne(s => s.Course)
            .HasForeignKey(s => s.CourseId);

            builder.Entity<Course>()
            .HasOne(c => c.Level)
            .WithMany()
            .HasForeignKey(c => c.LevelId);

            // Course_Teacher
            builder.Entity<CourseTeacher>().HasKey(ct => new { ct.CourseId, ct.TeacherId });
            builder.Entity<CourseTeacher>()
            .HasOne(ct => ct.Course)
            .WithMany(c => c.CourseTeachers)
            .HasForeignKey(c => c.CourseId);

            builder.Entity<CourseTeacher>()
            .HasOne(ct => ct.Teacher)
            .WithMany(c => c.CourseTeachers)
            .HasForeignKey(c => c.TeacherId);

            // Employee
            builder.Entity<Employee>().HasKey(e => e.Id);
            builder.Entity<Employee>()
            .HasOne(e => e.User)
            .WithOne(u => u.Employee)
            .HasForeignKey<Employee>(e => e.UserId);

            // FinalTest
            builder.Entity<FinalTest>().HasKey(ft => ft.Id);
            builder.Entity<FinalTest>()
            .HasOne(ft => ft.Teacher)
            .WithOne()
            .HasForeignKey<FinalTest>(ft => ft.TeacherId);

            // Lesson
            builder.Entity<Lesson>().HasKey(l => l.Id);

            builder.Entity<Lesson>()
            .HasOne(l => l.Course)
            .WithMany(c => c.Lessons)
            .HasForeignKey(l => l.CourseId);

            builder.Entity<Lesson>()
            .HasOne(l => l.Teacher)
            .WithMany(t => t.Lessons)
            .HasForeignKey(l => l.TeacherId);

            builder.Entity<Lesson>()
            .HasMany(l => l.LessonStudents)
            .WithOne(ls => ls.Lesson)
            .HasForeignKey(l => l.LessonId);

            // LessonStudent
            builder.Entity<LessonStudent>().HasKey(l => new { l.StudentId, l.LessonId });
            builder.Entity<LessonStudent>()
            .HasOne(ls => ls.Lesson)
            .WithMany(s => s.LessonStudents)
            .HasForeignKey(ls => ls.LessonId);

            // Level
            builder.Entity<Level>().HasKey(l => l.Id);

            //Placement_Test
            builder.Entity<PlacementTest>().HasKey(pt => pt.Id);

            builder.Entity<PlacementTest>()
            .HasOne(p => p.Booker) 
            .WithMany() 
            .HasForeignKey(p => p.BookerId); 

            builder.Entity<PlacementTest>()
            .HasOne(p => p.Consultant)
            .WithMany() 
            .HasForeignKey(p => p.ConsultantId);

            builder.Entity<PlacementTest>()
            .HasOne(p => p.Candidate)
            .WithMany()
            .HasForeignKey(p => p.CandidateId);

            builder.Entity<PlacementTest>()
            .HasOne(p => p.Teacher)
            .WithMany()
            .HasForeignKey(p => p.TeacherId);

            builder.Entity<PlacementTest>()
            .HasOne(p => p.Level)
            .WithMany()
            .HasForeignKey(p => p.OverrallScoreInLevel);

            // Role
            builder.Entity<Role>().HasKey(p => p.Id);
            builder.Entity<Role>()
            .HasMany(r => r.UserRoles)
            .WithOne(u => u.Role)
            .HasForeignKey(r => r.RoleId);

            //Student In Course
            builder.Entity<CourseCandidate>().HasKey(sc => sc.Id);
            builder.Entity<CourseCandidate>()
            .HasOne(s => s.Candidate)
            .WithMany(c => c.CourseCandidates)
            .HasForeignKey(s => s.CandidateId);

            builder.Entity<CourseCandidate>()
            .HasOne(s => s.Course)
            .WithMany(c => c.CourseCandidates)
            .HasForeignKey(s => s.CourseId);

            builder.Entity<CourseCandidate>()
            .HasOne(s => s.FinalTest)
            .WithOne()
            .HasForeignKey<CourseCandidate>(s => s.FinalTestId);

            // Teacher
            builder.Entity<Teacher>().HasKey(sc => sc.Id);
            builder.Entity<Teacher>()
            .HasMany(t => t.CourseTeachers)
            .WithOne(c => c.Teacher)
            .HasForeignKey(r => r.TeacherId);

            builder.Entity<Teacher>()
            .HasMany(t => t.Lessons)
            .WithOne(c => c.Teacher)
            .HasForeignKey(r => r.TeacherId);

            builder.Entity<Teacher>()
            .HasOne(t => t.Employee)
            .WithOne()
            .HasForeignKey<Teacher>(t => t.EmployeeId);

            // User
            builder.Entity<User>().HasKey(sc => sc.Id);
            builder.Entity<User>()
            .HasOne(t => t.Employee)
            .WithOne(e => e.User)
            .HasForeignKey<User>(e => e.EmployeeId);

            builder.Entity<User>()
            .HasMany(u => u.UserRoles)
            .WithOne(c => c.User)
            .HasForeignKey(r => r.UserId);

            // User Roles

            builder.Entity<UserRole>().HasKey(sc => new {sc.UserId, sc.RoleId});
            builder.Entity<UserRole>()
            .HasOne(ur => ur.User)
            .WithMany(u => u.UserRoles)
            .HasForeignKey(ur => ur.UserId);

            builder.Entity<UserRole>()
            .HasOne(ur => ur.Role)
            .WithMany(r => r.UserRoles)
            .HasForeignKey(ur => ur.RoleId);
        }
    }
}
