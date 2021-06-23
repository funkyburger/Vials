using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Vials.Shared;

namespace Vials.Core
{
    public interface IPathFinder
    {
        Task<IEnumerable<Pouring>> FindPath(VialSet set, CancellationToken token);
    }
}
