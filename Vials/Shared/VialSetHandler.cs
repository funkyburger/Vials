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
            set.HasChanged = false;
            set.LastAppliedPourings = new Pouring[] { };
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
            var appliedPourings = new List<Pouring>();

            if (from.IsEmpty)
            {
                return set;
            }

            var fromColor = from.TopColor;

            while (from.TopColor == fromColor && to.CanPour(fromColor))
            {
                to.Stack(from.Pop());
                appliedPourings.Add(new Pouring(indexFrom, indexTo));
                from.IsSelected = false;
                set.HasChanged = true;
            }

            set.LastAppliedPourings = appliedPourings;

            // ie pouring is done
            if (!from.IsSelected)
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
