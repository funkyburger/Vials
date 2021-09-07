using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public class SetMoveMaker : ISetMoveMaker
    {
        public void AppliMove(VialSet set, int fromFootPrint, int toFootPrint)
        {
            var from = set.Vials.Single(v => v.FootPrint == fromFootPrint);
            var to = set.Vials.Single(v => v.FootPrint == toFootPrint);

            var color = from.TopColor;

            while(from.CanPourFrom(color) && to.CanPour(color))
            {
                to.Stack(from.Pop());
            }
        }

        public VialSet CloneAndAppliMove(VialSet set, Pouring pouring)
        {
            throw new NotImplementedException();
        }
    }
}
