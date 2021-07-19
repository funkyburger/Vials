using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Exceptions
{
    public class MissingSeedException : Exception
    {
        public MissingSeedException(string message) : base(message)
        {
        }
    }
}
