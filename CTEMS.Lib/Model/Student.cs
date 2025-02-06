using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class Student : AuditEntity
    {
        public Student()
        {
        }
        public long CourseId { get; set; }
        public virtual Course Course { get; set; }
        public long CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public long? FinalTestId { get; set; }
        public virtual FinalTest FinalTest { get; set; }
    }
}
