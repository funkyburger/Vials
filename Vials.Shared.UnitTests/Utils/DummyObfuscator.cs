using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.UnitTests.Utils
{
    public class DummyObfuscator : IObfuscator
    {
        public long Obfuscate(long input, long key)
        {
            // -1 to check that obfuscation has been performed
            return key == 0 ? input - 1 : input + key;
        }

        public long Unobfuscate(long input, long key)
        {
            return key == 0 ? input + 1 : input - key;
        }
    }
}
