using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public class MoveTracker : IMoveTracker
    {
        private readonly IList<int> _stack = new List<int>();

        public void Stack(int fromFootPrint, int toFootPrint)
        {
            _stack.Add(toFootPrint);
            _stack.Add(fromFootPrint);
        }

        public IEnumerable<int> GetStack()
        {
            return _stack.Reverse();
        }
    }
}
