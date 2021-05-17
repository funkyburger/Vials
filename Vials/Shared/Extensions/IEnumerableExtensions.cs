using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Shared.Extensions
{
    public static class IEnumerableExtensions
    {
        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var random = new Random();
            var list = enumerable.ToList();

            while (list.Any())
            {
                var index = random.Next(0, list.Count());
                yield return list.ElementAt(index);
                list.RemoveAt(index);
            }
        }

        public static int FirstIndexOf<T>(this IEnumerable<T> enumerable, Func<T, bool> predicate)
        {
            var index = 0;

            foreach(var t in enumerable)
            {
                if (predicate(t))
                {
                    return index;
                }

                index++;
            }

            return -1;
        }
    }
}
