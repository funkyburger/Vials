using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Shared
{
    public class Pouring
    {
        public int From { get; private set; }
        public int To { get; private set; }
        public int Quantity { get; private set; }

        public Pouring(int from, int to, int quantity = 1)
        {
            From = from;
            To = to;
            Quantity = quantity;
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
