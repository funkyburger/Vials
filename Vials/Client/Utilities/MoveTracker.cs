using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public class MoveTracker : IMoveTracker
    {
        private readonly IList<long> _stack = new List<long>();

        public void Stack(long fromFootPrint, long toFootPrint, long ts)
        {
            _stack.Add(ts);
            _stack.Add(toFootPrint);
            _stack.Add(fromFootPrint);
        }

        public IEnumerable<long> GetStack()
        {
            return _stack.Reverse();
        }
    }
}
