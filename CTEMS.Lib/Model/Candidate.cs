using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class Candidate : AuditEntity
    {
        public Candidate()
        {
            PlacementTests = new HashSet<PlacementTest>();
            CourseCandidates = new HashSet<CourseCandidate>();
        }
        public string Name { get; set; }
        public DateTime? Dob { get; set; }
        public decimal? PhoneNum { get; set; }
        public string Email { get; set; }
        public decimal? IdentityNumber { get; set; }
        public bool IsDepositing { get; set; }
        public bool IsStudent { get; set; }
        public bool HasStudied { get; set; }
        public long? CurrentLevel { get; set; }
        public string EducationBg { get; set; }
        public string Field { get; set; }

        public virtual ICollection<PlacementTest> PlacementTests { get; set; }

        public virtual ICollection<CourseCandidate> CourseCandidates { get; set; }
    }
}
