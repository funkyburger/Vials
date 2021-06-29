using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Shared.Objects
{
    public class Pouring
    {
        public int From { get; set; }
        public int To { get; set; }
        public int Quantity { get; set; }

        public Pouring()
        {
            Quantity = 1;
        }

        public void Increment()
        {
            Quantity++;
        }

        public override string ToString()
        {
            if(Quantity == 1)
            {
                return $"{From} -> {To}";
            }
            else
            {
                return $"{From} -> {To} ({Quantity})";
            }
        }
    }
}
