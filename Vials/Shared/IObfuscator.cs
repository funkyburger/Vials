using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public interface IObfuscator
    {
        long Obfuscate(long input, long key);
        long Unobfuscate(long input, long key);
    }
}
