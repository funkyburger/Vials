using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Shared
{
    public class Pouring
    {
        public int From { get; private set; }
        public int To { get; private set; }

        public Pouring(int from, int to)
        {
            From = from;
            To = to;
        }
    }
}
