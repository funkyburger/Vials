using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared
{
    public class VialSet
    {
        public IEnumerable<Vial> Vials { get; set; }
        public bool HasChanged => LastAppliedPouring != null;
        public Pouring LastAppliedPouring { get; set; }

        public bool IsComplete 
        { 
            get 
            { 
                foreach(var vial in Vials)
                {
                    if(!(vial.IsComplete || vial.IsEmpty))
                    {
                        return false;
                    }
                }

                return true;
            } 
        }
    }
}
