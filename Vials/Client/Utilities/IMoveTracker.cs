using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public interface IMoveTracker
    {
        void Stack(int fromFootPrint, int toFootPrint);
        IEnumerable<int> GetStack();
    }
}
