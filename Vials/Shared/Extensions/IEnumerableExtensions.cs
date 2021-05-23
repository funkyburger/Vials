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

        public static bool IsEqualTo<T>(this IEnumerable<T> enumerable, IEnumerable<T> otherEnumerable)
        {
            if(otherEnumerable == null)
            {
                return false;
            }
            else if (enumerable.Count() != otherEnumerable.Count())
            {
                return false;
            }

            for(int i = 0; i < enumerable.Count(); i++)
            {
                if(!enumerable.ElementAt(i).Equals(otherEnumerable.ElementAt(i)))
                {
                    return false;
                }
            }

            return true;
        }
    }
}
