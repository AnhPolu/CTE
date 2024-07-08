using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json.Serialization;

namespace CTEMS.Lib.Model
{
    public class Role : BaseEntity
    {
        public Role()
        {
            UserRoles = new HashSet<UserRole>();
        }
        public string Name { get; set; }
        public string Code { get; set; }
        public virtual ICollection<UserRole> UserRoles { get; set; }

    }
}
