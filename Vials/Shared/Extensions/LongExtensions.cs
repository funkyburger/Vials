using System;
using System.Collections.Generic;
using System.Text;

namespace Vials.Shared.Extensions
{
    public static class LongExtensions
    {
        public static long MirrorBytes(this long input)
        {
            if(input >> 32 > 0)
            {
                return input;
            }

            var result = 0L;
            var temp = input;
            temp >>= 1; // first bit would overwrite sign bit.

            for(int i = 0; i < 63; i++)
            {
                result += temp % 2;

                temp >>= 1;
                result <<= 1;
            }

            result >>= 1;

            return result ^ input;
        }
    }
}
