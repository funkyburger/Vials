using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using Vials.Shared;

namespace Vials.Core.UnitTests.Utils
{
    public class ColorCounter : IEnumerable<KeyValuePair<Color, int>>
    {
        private IDictionary<Color, int> colorCounts = new Dictionary<Color, int>();

        public void Add(Color color)
        {
            int temp;
            if(colorCounts.TryGetValue(color, out temp))
            {
                colorCounts.Remove(color);
                colorCounts.Add(color, temp + 1);
            }
            else
            {
                colorCounts.Add(color, 1);
            }
        }

        public void Add(IEnumerable<Color> colors)
        {
            foreach(var color in colors)
            {
                Add(color);
            }
        }

        public void Add(VialSet set)
        {
            Add(EnumerateColors(set));
        }

        public int Count(Color color)
        {
            int temp;
            if (colorCounts.TryGetValue(color, out temp))
            {
                return temp;
            }
            else
            {
                return 0;
            }
        }

        private IEnumerable<Color> EnumerateColors(VialSet set)
        {
            foreach(var vial in set.Vials)
            {
                foreach (var color in vial.Colors)
                {
                    yield return color;
                }
            }
        }

        public IEnumerator<KeyValuePair<Color, int>> GetEnumerator()
        {
            return colorCounts.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return colorCounts.GetEnumerator();
        }
    }
}
