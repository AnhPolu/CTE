using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace CTEMS.Service.DTO
{
    public class EmployeeVM
    {
        public long Id { get; set; }
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
    }
}
