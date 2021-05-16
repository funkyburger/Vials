using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Vials.Core.Extensions
{
    public static class IEnumerableExtensions
    {
        //public static T Blah<T>(this IEnumerable<T> enumerable)
        //{
        //    enumerable.
        //}

        public static IEnumerable<T> Shuffle<T>(this IEnumerable<T> enumerable)
        {
            var random = new Random();
            var list = enumerable.ToList();

            //list.ell


            while (list.Any())
            {
                var index = random.Next(0, list.Count());
                yield return list.ElementAt(index);
                list.RemoveAt(index);
            }
        }
    }
}
