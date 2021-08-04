using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Shared;
using Vials.Shared.Exceptions;
using Vials.Shared.Objects;

namespace Vials.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FinishController : ControllerBase
    {
        //private readonly IFinishedGamePacker _checker;
        private readonly IFinishedGamePacker _packer;

        public FinishController(IFinishedGamePacker packer)
        {
            _packer = packer;
        }

        [HttpPost]
        [Route("check")]
        public int Check(IEnumerable<long> pack)
        {
            var game = _packer.Unpack(pack);

            return 0;
        }
    }
}