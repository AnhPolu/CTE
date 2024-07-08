using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common.Attributes
{
    public class CodeAttribute : Attribute
    {
        public string Text { get; private set; }

        public CodeAttribute(string text)
        {
            Text = text;
        }
    }
}
