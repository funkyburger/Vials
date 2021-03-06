using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Extensions;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public class VialSetHandler : IVialSetHandler
    {
        public VialSet Select(VialSet set, int index)
        {
            set.LastAppliedPouring = null;
            var selected = set.Vials.SingleOrDefault(v => v.IsSelected);

            if (selected != null)
            {
                var target = set.Vials.ElementAt(index);

                if (target.IsSelected)
                {
                    target.IsSelected = false;
                    foreach (var vial in set.Vials)
                    {
                        vial.IsOption = false;
                    }
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
                if (target.IsEmpty)
                {
                    return set;
                }
                
                target.IsSelected = true;
                return MarkOptions(set, target);
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

            var fromColor = from.TopColor;

            while (from.TopColor == fromColor && to.CanPour(fromColor))
            {
                to.Stack(from.Pop());
                from.IsSelected = false;

                if(set.LastAppliedPouring == null)
                {
                    set.LastAppliedPouring = new Pouring() { 
                        From = indexFrom,
                        To = indexTo
                    };
                }
                else
                {
                    set.LastAppliedPouring.Increment();
                }
            }

            if (set.HasChanged)
            {
                foreach(var vial in set.Vials)
                {
                    vial.IsOption = false;
                }
            }

            return set;
        }

        private VialSet MarkOptions(VialSet set, Vial selected)
        {
            var color = selected.TopColor;

            foreach(var vial in set.Vials)
            {
                vial.IsOption = !vial.IsSelected && vial.CanPour(color);
            }

            return set;
        }
    }
}
