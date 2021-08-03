using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Server.Utilities;

namespace Vials.Server.UnitTests.Utils
{
    public class FakeRandomGenerator : IRandomGenerator
    {
        private readonly int[] _comingValues;
        private int index = 0;

        public FakeRandomGenerator()
        {
            _comingValues = new int[100];
            for (int i = 1; i <= 100; i++)
            {
                _comingValues[i - 1] = i;
            }
        }

        public FakeRandomGenerator(IEnumerable<int> comingValues)
        {
            _comingValues = comingValues.ToArray();
        }

        public int Generate(int seed)
        {
            index++;
            return _comingValues[index - 1];
        }

        public int Generate(int seed, int maxValue)
        {
            index++;
            return _comingValues[index - 1] % maxValue;
        }

        public int GenerateNext()
        {
            index++;
            return _comingValues[index - 1];
        }

        public int GenerateNext(int maxValue)
        {
            index++;
            return _comingValues[index - 1] % maxValue;
        }
    }
}
