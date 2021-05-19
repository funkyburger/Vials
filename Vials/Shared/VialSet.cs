using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public class VialSet : IVialSet
    {
        public IEnumerable<Vial> Vials { get; set; }

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
