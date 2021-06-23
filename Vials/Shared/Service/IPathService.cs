using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vials.Shared.Service
{
    // TODO move to client
    public interface IPathService
    {
        Task<IEnumerable<Pouring>> FetchPath(VialSet set);
    }
}
