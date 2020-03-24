using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedModels;

namespace ValidatieBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdresController : ControllerBase
    {
        private readonly ILogger<AdresController> _logger;

        public AdresController(ILogger<AdresController> logger)
        {
            _logger = logger;
        }

        [HttpGet]
        public AdresModel Get()
        {

        }
    }
}
