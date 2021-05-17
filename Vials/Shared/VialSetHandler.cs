using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared
{
    public class VialSetHandler : IVialSetHandler
    {
        public VialSet Select(VialSet set, int index)
        {
            //var ppi = set.Vials.ToList();
            //ppi.
            var selected = set.Vials.SingleOrDefault(v => v.IsSelected);

            if(selected != null)
            {
                var target = set.Vials.ElementAt(index);

                if (target.IsSelected)
                {
                    target.IsSelected = false;
                    return set;
                }
            }
            else
            {
                var target = set.Vials.ElementAt(index);
                target.IsSelected = true;
                return set;
            }

            throw new NotImplementedException();
        }

        //private Vial Selected()
        //public VialSet Pour(VialSet set, int fromIndex, int toIndex)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
