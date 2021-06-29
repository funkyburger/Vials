using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared.Extensions
{
    public static class VialSetExtensions
    {
        public static bool ContentEquals(this VialSet set, VialSet otherSet)
        {
            if(otherSet == null)
            {
                return false;
            }
            else if (set.Vials.Count() != otherSet.Vials.Count())
            {
                return false;
            }

            for (int i = 0; i < set.Vials.Count(); i++)
            {
                if (!set.Vials.ElementAt(i).Colors.IsEqualTo(otherSet.Vials.ElementAt(i).Colors))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
