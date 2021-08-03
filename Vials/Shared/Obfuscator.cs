using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared
{
    public class Obfuscator : IObfuscator
    {
        private const long _magicNumber = 1342904482896021812L;

        public long Obfuscate(long input, long key)
        {
            return (input ^ key) ^ _magicNumber;
        }

        public long Unobfuscate(long input, long key)
        {
            return (input ^ _magicNumber) ^ key;
        }
    }
}
