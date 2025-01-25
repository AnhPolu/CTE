using System;
using System.Collections.Generic;
using System.Text;

namespace CTEMS.Service.DTO
{
    public class TeacherVM
    {
        public int Id { get; set; }
        public long RoleId { get; set; }
        public string FirstName { get; set; }
        public string MiddleName { get; set; }
        public string LastName { get; set; }
        public DateTime? Dob { get; set; }
        public decimal? IdentityNumber { get; set; }
        public decimal? PhoneNumber { get; set; }
        public string Address { get; set; }
        public string Email { get; set; }
        public double? Ielts { get; set; }
        public double? SpeakingScore { get; set; }
        public double? WritingScore { get; set; }
        public double? ReadingScore { get; set; }
        public double? ListeningScore { get; set; }
        public DateTime? SignOnDate { get; set; }
        public long? UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public long CreatedBy { get; set; }
        public DateTime? ModifiedDate { get; set; }
        public long? ModifiedBy { get; set; }
    }
}
