using System;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public class PlacementTest : AuditEntity
    {
        public DateTime? TestTime { get; set; }
        public int? OverrallScoreInPoint { get; set; }
        public long? OverrallScoreInLevel { get; set; }
        public decimal? DepositAmount { get; set; }
        public string Note { get; set; }
        public string Others { get; set; }
        public long CandidateId { get; set; }
        public virtual Candidate Candidate { get; set; }
        public long? TeacherId { get; set; }
        public virtual Teacher Teacher{ get; set; }
        public long BookerId { get; set; }
        public virtual Employee Booker { get; set; }
        public long? ConsultantId { get; set; }
        public virtual Employee Consultant { get; set; }
        public virtual Level Level { get; set; }

    }
}
