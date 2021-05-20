using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public class VialSetHistory : IVialSetHistory
    {
        private readonly IList<VialSet> vialSets = new List<VialSet>();
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
    }
}
