using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared
{
    public class VialSetHistory : IVialSetHistory
    {
        private readonly IList<Pouring> _pourings = new List<Pouring>();
        private int currentIndex = -1;

        public void RegisterMove(Pouring pourings)
        {
            ClearUpcoming();

            _pourings.Add(pourings);
            currentIndex++;
        }

        public void SetOngoingPath(IEnumerable<Pouring> path)
        {
            Console.WriteLine("SetOngoingPath");

            if (path == null)
                Console.WriteLine("path is null");

            throw new NotImplementedException();
        }

        public VialSet Undo(VialSet set)
        {
            if (CanUndo)
            {
                RevertMove(set, _pourings[currentIndex]);
                currentIndex--;
            }

            return set;
        }

        public VialSet Redo(VialSet set)
        {
            if (CanRedo)
            {
                currentIndex++;
                ApplyMove(set, _pourings[currentIndex]);
            }

            return set;
        }

        public bool CanUndo => currentIndex >= 0;

        public bool CanRedo => currentIndex < _pourings.Count() - 1;

        public void Reset()
        {
            _pourings.Clear();
            currentIndex = -1;
        }

        private void ClearUpcoming()
        {
            if(currentIndex < 0)
            {
                _pourings.Clear();
                return;
            }

            while (currentIndex + 1 < _pourings.Count())
            {
                _pourings.RemoveAt(currentIndex + 1);
            }
        }

        private void ApplyMove(VialSet set, Pouring pouring)
        {
            for (int i = 0; i < pouring.Quantity; i++)
            {
                set.Vials.ElementAt(pouring.To).Stack(
                    set.Vials.ElementAt(pouring.From).Pop());
            }
        }

        private void RevertMove(VialSet set, Pouring pouring)
        {
            for(int i = 0; i < pouring.Quantity; i++)
            {
                set.Vials.ElementAt(pouring.From).Stack(
                    set.Vials.ElementAt(pouring.To).Pop());
            }
        }
    }
}
