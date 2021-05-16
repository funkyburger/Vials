using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public class VialClickedHandler : IClickHandler
    {
        private readonly IVialSet VialSet;

        public VialClickedHandler(IVialSet vialSet)
        {
            VialSet = vialSet;
        }

        public void Handle(object sender)
        {
            via

            //throw new NotImplementedException();
            Console.WriteLine("VialClickedHandled");
        }
    }
}
