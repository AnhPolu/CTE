using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CTEMS.Service.DTO
{
    public class UserVM
    {
        public long Id { get; set; }
        public decimal? Salt { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string AccessRights { get; set; }
        public bool RequiredToChangePass { get; set; }
        public string DefaultResetPassword { get; set; }
        public long EmployeeId { get; set; }
    }
}
