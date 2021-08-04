﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared.Exceptions;

namespace Vials.Server.Utilities
{
    public class PseudoRandomGenerator : IRandomGenerator
    {
        private const int dummy = 2114319875;
        private int previous = 0;
        private int? internalSeed = null;

        public int Generate(int seed)
        {
            internalSeed = seed;
            previous = dummy;
            return GenerateNext();
        }

        public int Generate(int seed, int maxValue)
        {
            return MakePositiveAndBound(Generate(seed), maxValue);
        }

        public int GenerateNext()
        {
            if (!internalSeed.HasValue)
            {
                throw new RandomGenerationException("Generator was not seeded");
            }

            var temp = internalSeed.Value;
            internalSeed = (internalSeed.Value << 1) ^ previous >> 1;
            previous = temp;
            return internalSeed.Value;
        }

        public int GenerateNext(int maxValue)
        {
            return MakePositiveAndBound(GenerateNext(), maxValue);
        }

        private int MakePositiveAndBound(int randomValue, int maxValue)
        {
            return Math.Abs(randomValue) % maxValue;
        }
    }
}