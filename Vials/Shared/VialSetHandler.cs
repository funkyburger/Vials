using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;

namespace Vials.Shared
{
    public class VialSetHandler : IVialSetHandler
    {
        public VialSet Select(VialSet set, int index)
        {
            var selected = set.Vials.SingleOrDefault(v => v.IsSelected);

            if (selected != null)
            {
                var target = set.Vials.ElementAt(index);

                if (target.IsSelected)
                {
                    target.IsSelected = false;
                    return set;
                }
                else
                {
                    return TryPouring(set, 
                        set.Vials.FirstIndexOf(v => v.IsSelected),
                        index);
                }
            }
            else
            {
                var target = set.Vials.ElementAt(index);
                target.IsSelected = true;
                return set;
            }
        }

        private VialSet TryPouring(VialSet set, int indexFrom, int indexTo)
        {
            var from = set.Vials.ElementAt(indexFrom);
            var to = set.Vials.ElementAt(indexTo);

            if (from.IsEmpty)
            {
                return set;
            }

            if (!to.IsEmpty && from.TopColor != to.TopColor)
            {
                return set;
            }

            from.IsSelected = false;

            while ((from.TopColor == to.TopColor || to.IsEmpty) && !to.IsFull)
            {
                to.Stack(from.Pop());
            }

            return set;
        }
    }
}
