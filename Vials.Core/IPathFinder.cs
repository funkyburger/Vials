using System;
using System.Collections.Generic;
using System.Text;
using Vials.Shared;

namespace Vials.Core
{
    public interface IPathFinder
    {
        IEnumerable<Pouring> FindPath(VialSet set);
    }
}
