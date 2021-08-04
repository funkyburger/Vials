using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Objects
{
    public class FinishedGame
    {
        public IEnumerable<long> Track { get; set; }
        public int Seed { get; set; }
        public int FootPrint { get; set; }
    }
}
