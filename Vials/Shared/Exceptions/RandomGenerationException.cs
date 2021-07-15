using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Exceptions
{
    public class RandomGenerationException : Exception
    {
        public RandomGenerationException(string message) : base(message)
        {
        }
    }
}
