using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Exceptions
{
    public class GameCheckException : Exception
    {
        public GameCheckException(string message) : base(message)
        {
        }
    }
}
