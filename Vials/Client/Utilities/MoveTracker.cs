using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Extensions;

namespace Vials.Client.Utilities
{
    public class MoveTracker : IMoveTracker
    {
        // TODO backtracking support

        private readonly IList<long> _stack = new List<long>();

        public void Stack(long fromFootPrint, long toFootPrint, long ts)
        {
            _stack.Add(fromFootPrint);
            _stack.Add(toFootPrint);
            _stack.Add(ts);
        }

        public IEnumerable<long> GetStack()
        {
            return _stack;
        }
    }
}
