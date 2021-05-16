using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared
{
    public class Vial
    {
        public static readonly int Length = 4;

        public IEnumerable<Color> Colors { get; set; }
        public bool IsSelected { get; set; }
        public bool IsOption { get; set; }

        public override string ToString()
        {
            var builder = new StringBuilder();

            foreach(var color in Colors)
            {
                builder.Append(color.ToString());
            }

            return builder.ToString();
        }
    }
}
