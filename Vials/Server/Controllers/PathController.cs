using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Vials.Core;
using Vials.Shared.Objects;

namespace Vials.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PathController : ControllerBase
    {
        private readonly IPathFinder _pathFinder;

        public PathController(IPathFinder pathFinder)
        {
            _pathFinder = pathFinder;
        }

        [HttpPost]
        [Route("find")]
        public async Task<IEnumerable<Pouring>> FindPath(VialSet set)
        {
            var source = new CancellationTokenSource();
            source.CancelAfter(120000);

            return await _pathFinder.FindPath(set, source.Token);
        }
    }
}
