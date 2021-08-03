using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public interface IMoveTracker
    {
        void Stack(long fromFootPrint, long toFootPrint, long ts);
        IEnumerable<long> GetStack();
    }
}
