using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Vials.Server.Utilities;
using Vials.Shared.Exceptions;
using Vials.Shared.Objects;

namespace Vials.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VialController : ControllerBase
    {
        private readonly ISetGenerator _setGenerator;

        public VialController(ISetGenerator setGenerator)
        {
            _setGenerator = setGenerator;
        }

        [HttpGet]
        [Route("new")]
        public VialSet New(int? seed)
        {
            if (!seed.HasValue)
            {
                throw new MissingSeedException("Seed for new game was not provided.");
            }

            return _setGenerator.Generate(4, 2, seed.Value);
        }
    }
}
