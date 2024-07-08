using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class Course : AuditEntity
    {
        public Course()
        {
            Lessons = new HashSet<Lesson>();
            CourseCandidates = new HashSet<CourseCandidate>();
            CourseTeachers = new HashSet<CourseTeacher>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool? IsDeleted { get; set; }
        public bool? IsCompleted { get; set; }
        public string Localtion { get; set; }
        public long LevelId { get; set; }
        public virtual Level Level { get; set; }
        public virtual ICollection<CourseTeacher> CourseTeachers { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
        public virtual ICollection<CourseCandidate> CourseCandidates { get; set; }
    }
}
