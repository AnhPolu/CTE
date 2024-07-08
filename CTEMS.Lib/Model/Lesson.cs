using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class Lesson : AuditEntity
    {
        public Lesson()
        {
            LessonStudents = new HashSet<LessonStudent>();
        }
        public long CourseId { get; set; }
        public long TeacherId { get; set; }
        public DateTime? StartedDateTime { get; set; }
        public DateTime? EndDateTime { get; set; }
        public string Topic { get; set; }
        public string Note { get; set; }
        public bool? IsCancel { get; set; }
        public virtual Course Course { get; set; }
        public virtual Teacher Teacher { get; set; }
        public virtual ICollection<LessonStudent> LessonStudents { get; set; }
    }
}
