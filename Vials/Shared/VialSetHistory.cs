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
        private readonly IList<IEnumerable<Pouring>> _pourings = new List<IEnumerable<Pouring>>();
        //private VialSet _original = null;
        //private VialSet _current = null;
        private int currentIndex = -1;

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

        public void RegisterMove(IEnumerable<Pouring> pourings)
        {
            _pourings.Add(pourings);
            currentIndex++;
        }

        public VialSet GetPrevious()
        {
            if(currentIndex > 0)
            {
                currentIndex--;
                return Current;
            }

            return null;
        }

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
            if (currentIndex > 0)
            {
                currentIndex--;
                RevertMove(set, _pourings[currentIndex]);
            }

            return set;
        }

        public VialSet Redo(VialSet set)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            vialSets.Clear();
            currentIndex = -1;
        }

        private void ClearUpcoming()
        {
            if (currentIndex < 0)
            {
                return;
            }

            while (currentIndex + 1 < vialSets.Count)
            {
                vialSets.RemoveAt(currentIndex + 1);
            }
        }

        private void ApplyMove(VialSet set, IEnumerable<Pouring> pourings)
        {

        }

        private void ApplyMove(VialSet set, Pouring pouring)
        {

        }

        private void RevertMove(VialSet set, IEnumerable<Pouring> pourings)
        {
            foreach (var pouring in pourings.Reverse())
            {
                RevertMove(set, pouring);
            }
        }

        private void RevertMove(VialSet set, Pouring pouring)
        {
            set.Vials.ElementAt(pouring.From).Stack(
                set.Vials.ElementAt(pouring.To).Pop());
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
