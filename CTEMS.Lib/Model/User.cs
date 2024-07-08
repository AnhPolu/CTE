using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CTEMS.Lib.Model
{
    public class User : IBaseEntity
    {
        public User() {
            UserRoles = new HashSet<UserRole>();
        }
        public long Id { get; set; }
        [JsonIgnore]
        public string Password { get; set; }
        public decimal? Salt { get; set; }
        public string UserName { get; set; }
        public string AccessRights { get; set; }
        public bool RequiredToChangePass { get; set; }
        public string DefaultResetPassword { get; set; }
        public long EmployeeId { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
