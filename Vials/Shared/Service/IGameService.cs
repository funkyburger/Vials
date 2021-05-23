using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Vials.Shared.Service
{
    public interface IGameService
    {
        Task<VialSet> GetNewGame();
    }
}
