using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Objects
{
    public class HistoryExport
    {
        public IEnumerable<Pouring> Pourings { get; set; }
        public int Current { get; set; }
    }
}
