using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared
{
    public class VialSetHistory : IVialSetHistory
    {
        private readonly IList<VialSet> vialSets = new List<VialSet>();
        private readonly IList<Pouring> _pourings = new List<Pouring>();
        //private VialSet _original = null;
        //private VialSet _current = null;
        private int currentIndex = -1;

        [Obsolete]
        public VialSet Current 
        {
            get
            {
                if(currentIndex >= 0)
                {
                    return vialSets[currentIndex];
                }

                return null;
            }
        }

        [Obsolete]
        public void Store(VialSet set)
        {
            ClearUpcoming();

            vialSets.Add(set);
            currentIndex++;
        }

        //public void SetOriginal(VialSet set)
        //{
        //    _original = set;
        //    _current = _original;
        //}

        public void RegisterMove(Pouring pourings)
        {
            ClearUpcoming();

            _pourings.Add(pourings);
            currentIndex++;
        }

        [Obsolete]
        public VialSet GetPrevious()
        {
            if(currentIndex > 0)
            {
                currentIndex--;
                return Current;
            }

            return null;
        }

        [Obsolete]
        public VialSet GetNext()
        {
            if(currentIndex < vialSets.Count - 1)
            {
                currentIndex++;
                return Current;
            }

            return null;
        }

        public VialSet Undo(VialSet set)
        {
            if (currentIndex >= 0)
            {
                RevertMove(set, _pourings[currentIndex]);
                currentIndex--;
            }

            return set;
        }

        public VialSet Redo(VialSet set)
        {
            if (currentIndex < _pourings.Count() - 1)
            {
                currentIndex++;
                ApplyMove(set, _pourings[currentIndex]);
            }

            return set;
        }

        public void Clear()
        {
            vialSets.Clear();
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

        //private void ApplyMove(VialSet set, IEnumerable<Pouring> pourings)
        //{

        //}

        private void ApplyMove(VialSet set, Pouring pouring)
        {
            for (int i = 0; i < pouring.Quantity; i++)
            {
                set.Vials.ElementAt(pouring.To).Stack(
                    set.Vials.ElementAt(pouring.From).Pop());
            }
        }

        //private void RevertMove(VialSet set, IEnumerable<Pouring> pourings)
        //{
        //    foreach (var pouring in pourings.Reverse())
        //    {
        //        RevertMove(set, pouring);
        //    }
        //}

        private void RevertMove(VialSet set, Pouring pouring)
        {
            for(int i = 0; i < pouring.Quantity; i++)
            {
                set.Vials.ElementAt(pouring.From).Stack(
                    set.Vials.ElementAt(pouring.To).Pop());
            }
        }

        public IEnumerator<VialSet> GetEnumerator()
        {
            return vialSets.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return vialSets.GetEnumerator();
        }
    }
}
