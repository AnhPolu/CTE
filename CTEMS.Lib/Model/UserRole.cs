using System;
using System.Collections.Generic;
using System.Text;

namespace CTEMS.Lib.Model
{
    public class UserRole
    {
        public long RoleId { get; set; }
        public long UserId { get; set; }
        public virtual User User{ get; set; }
        public virtual Role Role{ get; set; }
    }
}
