using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Vials.Client.Utilities
{
    public class LinkHelper : ILinkHelper
    {
        private readonly NavigationManager _navigationManager;

        public LinkHelper(NavigationManager navigationManager)
        {
            _navigationManager = navigationManager;
        }

        public void NavigateToRandomGame()
        {
            NavigateToSpecificGame(new Random().Next());
        }

        public void NavigateToSpecificGame(int seed)
        {
            _navigationManager.NavigateTo($"/{seed}");
        }
    }
}
