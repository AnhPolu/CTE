using System;
using System.Collections.Generic;
using System.Text;

namespace CTEMS.Lib.Model
{
    public class CourseTeacher
    {
        public CourseTeacher() { }
        public string IsMain { get; set; }
        public long TeacherId { get; set; }
        public virtual Teacher Teacher{ get; set; }
        public long CourseId { get; set; }
        public virtual Course Course { get; set; }
    }   
}
