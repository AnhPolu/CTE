using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    public class ValueAttribute: Attribute
    {
        public int EnumValue { get; private set; }

        public ValueAttribute(int value)
        {
            EnumValue = value;
        }
    }
}
