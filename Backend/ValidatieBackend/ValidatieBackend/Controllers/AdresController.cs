using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using SharedModels;
using ValidatieBackend.services;

namespace ValidatieBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdresController : ControllerBase
    {
        private readonly ILogger<AdresController> logger;
        private IAdresService Service { get; set; }


        public AdresController(ILogger<AdresController> logger, IAdresService adresService)
        {
            this.logger = logger;
            Service = adresService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public ActionResult<AdresModel> Get(int id)
        {
            var result = Service.GetAdresById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        public async Task<ActionResult<AdresModel>> PostAdres(AdresModel model)
        {
            var validation = await new AdresValidator().ValidateAsync(model);
            if (!validation.IsValid)
            {
                throw new ValidationException(validation.ToString());
            }

            var result = await Service.CreateAdres(model);

            return CreatedAtAction(nameof(Get), new {id = result.Id}, result);
        }
    }
}