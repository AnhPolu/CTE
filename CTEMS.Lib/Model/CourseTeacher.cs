using System;
using System.Collections.Generic;
using System.Text;

namespace CTEMS.Lib.Model
{
    public class CourseTeacher : AuditEntity
    {
        public CourseTeacher() { }
        public string IsMain { get; set; }
        public long TeacherId { get; set; }
        public virtual Teacher Teacher{ get; set; }
        public long CourseId { get; set; }
        public virtual Course Course { get; set; }
        public long FeedBackId { get; set; }
        public virtual FeedBack FeedBack { get; set; }
    }   
}
