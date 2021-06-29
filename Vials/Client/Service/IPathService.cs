using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Vials.Shared.Objects;

namespace Vials.Client.Service
{
    public interface IPathService
    {
        Task<IEnumerable<Pouring>> FetchPath(VialSet set);
    }
}
