using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Server.Game.Check;
using Vials.Shared;
using Vials.Shared.Exceptions;
using Vials.Shared.Objects;

namespace Vials.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishController : ControllerBase
    {
        private readonly IFinishedGamePacker _packer;
        private readonly IFinishedGamerChecker _checker;

        public FinishController(IFinishedGamePacker packer, IFinishedGamerChecker checker)
        {
            _packer = packer;
            _checker = checker;
        }

        [HttpPost]
        [Route("check")]
        public IActionResult Check(IEnumerable<long> pack)
        {
            var game = _packer.Unpack(pack);

            try
            {
                _checker.Check(game);
            }
            catch (GameCheckException)
            {
                return BadRequest();
            }

            return Ok();
        }
    }
}