using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Vials.Shared;

namespace Vials.Core
{
    public class PathFinder : IPathFinder
    {
        private readonly ICloner _cloner;
        private readonly IDictionary<VialSet, int> setRegister = new Dictionary<VialSet, int>();

        public PathFinder(ICloner cloner)
        {
            _cloner = cloner;
        }

        public IEnumerable<Pouring> FindPath(VialSet set)
        {
            var path = FindPath(set, 0);
            if(path != null)
            {
                return path.Reverse();
            }

            return null;
        }

        public IEnumerable<Pouring> FindPath(VialSet set, int step)
        {
            if(step > 50)
            {
                return null;
            }
            if (setRegister.ContainsKey(set))
            {
                return null;
            }

            setRegister.Add(set, 0);

            var moves = ListAvailableMoves(set);

            foreach (var move in moves)
            {
                var temp = AppliMove(set, move);

                if (temp.IsComplete)
                {
                    return new List<Pouring>(new Pouring[] { move });
                }
                else
                {
                    var path = FindPath(temp, step + 1);
                    if(path != null)
                    {
                        var list = path as IList<Pouring>;
                        list.Add(move);
                        return list;
                    }
                }
            }

            return null;
        }

        private VialSet AppliMove(VialSet set, Pouring pouring)
        {
            var clone = _cloner.Clone(set);

            for(int i = 0; i < pouring.Quantity; i++)
            {
                clone.Vials.ElementAt(pouring.To).Stack(clone.Vials.ElementAt(pouring.From).Pop());
            }

            return clone;
        }

        private IEnumerable<Pouring> ListAvailableMoves(VialSet set)
        {
            for(int i = 0; i < set.Vials.Count(); i++)
            {
                var topColor = set.Vials.ElementAt(i).TopColor;
                if(topColor == default)
                {
                    continue;
                }

                for (int j = 0; j < set.Vials.Count(); j++)
                {
                    if(i == j)
                    {
                        continue;
                    }

                    var pouringCount = GetPouringCount(set.Vials.ElementAt(i), set.Vials.ElementAt(j));
                    if(pouringCount > 0)
                    {
                        yield return new Pouring(i, j, pouringCount);
                    }
                }
            }
        }

        private int GetPouringCount(Vial from, Vial to)
        {
            if (!to.CanPour(from.TopColor))
            {
                return 0;
            }

            return Math.Min(TopColorCount(from), IdleSpaceCount(to));
        }

        private int TopColorCount(Vial vial)
        {
            int count = 0;
            var topColor = vial.TopColor;
            for(int i = vial.Colors.Count() - 1; i >= 0; i--)
            {
                if(vial.Colors.ElementAt(i) == topColor)
                {
                    count++;
                }
                else
                {
                    break;
                }
            }

            return count;
        }

        private int IdleSpaceCount(Vial vial)
        {
            return Vial.Length - vial.Colors.Count();
        }
    }
}
