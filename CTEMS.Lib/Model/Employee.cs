using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class Employee : AuditEntity
    {
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        [Range(typeof(decimal?), "0", "99999999999999999999")]
        public decimal? IdentityNumber { get; set; }
        [Range(typeof(decimal?), "0", "9999999999999")]
        public decimal? PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public DateTime? SignOnDate { get; set; }
        public long? UserId { get; set; }
        public virtual User User { get; set; }
    }
}
