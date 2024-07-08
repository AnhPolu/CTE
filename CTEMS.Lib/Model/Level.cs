
using System;
using System.Collections.Generic;

// Code scaffolded by EF Core assumes nullable reference types (NRTs) are not used or disabled.
// If you have enabled NRTs for your project, then un-comment the following line:
// #nullable disable

namespace CTEMS.Lib.Model
{
    public partial class Level : AuditEntity
    {
        public Level()
        {
        }
        public string Name { get; set; }
        public string Code { get; set; }
    }
}
