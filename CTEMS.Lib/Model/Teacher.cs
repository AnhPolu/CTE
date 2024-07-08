using System;
using System.Collections.Generic;

namespace CTEMS.Lib.Model
{
    public partial class Teacher : AuditEntity
    {
        public Teacher()
        {
            CourseTeachers = new HashSet<CourseTeacher>();
        }
        public double? Ielts { get; set; }
        public double? SpeakingScore { get; set; }
        public double? WritingScore { get; set; }
        public double? ReadingScore { get; set; }
        public double? ListeningScore { get; set; }
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<CourseTeacher> CourseTeachers { get; set; }
        public virtual ICollection<Lesson> Lessons { get; set; }
    }
}
