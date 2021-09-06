using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Extensions;

namespace Vials.Client.Utilities
{
    public class MoveTracker : IMoveTracker
    {
        // TODO support of backtracking

        private readonly IList<long> _stack = new List<long>();

        public void Stack(long fromFootPrint, long toFootPrint, long ts)
        {
            Console.WriteLine("tracker : " + ts.ToTimeString());

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
