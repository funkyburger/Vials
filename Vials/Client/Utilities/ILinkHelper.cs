using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public interface ILinkHelper
    {
        void NavigateToRandomGame();
        void NavigateToSpecificGame(int seed);
    }
}
