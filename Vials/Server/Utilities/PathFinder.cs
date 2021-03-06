using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vials.Shared;
using Vials.Shared.Objects;

namespace Vials.Server.Utilities
{
    public class PathFinder : IPathFinder
    {
        private readonly ICloner _cloner;
        private readonly IDictionary<int, int> setRegister = new Dictionary<int, int>();
        private int _maxPathLength = 50;

        public PathFinder(ICloner cloner)
        {
            _cloner = cloner;
        }

        public async Task<IEnumerable<Pouring>> FindPath(VialSet set, CancellationToken token)
        {
            var path = await FindPath(set, 0, token);
            if (path != null)
            {
                return path.Reverse();
            }

            return null;
        }

        public async Task<IEnumerable<Pouring>> FindPath(VialSet set, int length, CancellationToken token)
        {
            if (token.IsCancellationRequested)
            {
                return null;
            }

            IEnumerable<Pouring> shortestPath = null;

            if (length >= _maxPathLength)
            {
                return null;
            }
            if (setRegister.ContainsKey(set.GetHashCode()))
            {
                return null;
            }

            setRegister.Add(set.GetHashCode(), 0);

            var moves = ListAvailableMoves(set);

            foreach (var move in moves)
            {
                var temp = AppliMove(set, move);

                if (temp.IsComplete)
                {
                    _maxPathLength = length;
                    return new List<Pouring>(new Pouring[] { move });
                }
                else
                {
                    var path = await FindPath(temp, length + 1, token);
                    if (path != null)
                    {
                        if (shortestPath == null)
                        {
                            shortestPath = path.Append(move);
                        }
                        else if (shortestPath.Count() > path.Count() + 1)
                        {
                            shortestPath = path.Append(move);
                        }
                    }
                }
            }

            return shortestPath;
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
                        yield return new Pouring() { 
                            From = i,
                            To = j,
                            Quantity = pouringCount
                        };
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
