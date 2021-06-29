using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared.Objects;

namespace Vials.Shared
{
    public class Cloner : ICloner
    {
        public Vial Clone(Vial vial)
        {
            return new Vial(vial.Colors);
        }

        public VialSet Clone(VialSet vialSet)
        {
            var set = new VialSet();
            var vialList = new List<Vial>();

            foreach (var vial in vialSet.Vials)
            {
                vialList.Add(Clone(vial)); ;
            }

            set.Vials = vialList;

            return set;
        }
    }
}
