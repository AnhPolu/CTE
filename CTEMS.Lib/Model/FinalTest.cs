using Microsoft.AspNetCore.DataProtection.KeyManagement;
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class FinalTest : AuditEntity
    {
        public FinalTest()
        {
        }
        public decimal? DepositAmount { get; set; }
        public long? TeacherId { get; set; }
        public int? OverrallScoreInPoint { get; set; }
        public long? OverrallScoreInLevel { get; set; }
        public string Others { get; set; }
        public string Note { get; set; }
        public virtual Teacher Teacher { get; set; }
    }
}
