using Common.Attributes;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Enums
{
    public enum CTERole
    {
        [Description("Admin"), Code("ADMIN"), Value(1)]
        Admin,
        [Description("RTO"), Code("RTO"), Value(2)]
        RTO,
        [Description("Trainer"), Code("TRAINER"), Value(3)]
        Trainer,
        [Description("Student"), Code("STUDENT"), Value(4)]
        Student,
        [Description("Client"), Code("CLIENT"), Value(5)]
        Client = 5,
        [Description("Enterprise"), Code("ENTERPRISE"), Value(6)]
        Enterprise = 6
    }
}
