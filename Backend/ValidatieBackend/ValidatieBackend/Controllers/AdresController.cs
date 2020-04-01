using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SharedModels;
using System.Threading.Tasks;
using ValidatieBackend.services;

// https://johnthiriet.com/efficient-api-calls/
namespace ValidatieBackend.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class AdresController : ControllerBase
    {
        private IAdresService Service { get; set; }


        public AdresController(IAdresService adresService)
        {
            Service = adresService;
        }

        [HttpGet("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AdresModel>> Get(int id)
        {
            var result = await Service.GetAdresById(id);
            if (result == null)
            {
                return NotFound();
            }

            return Ok(result);
        }

        [HttpPost]
        [ProducesResponseType(StatusCodes.Status201Created)]
        public async Task<ActionResult<AdresModel>> PostAdres(AdresModel model)
        {
            var validation = await new AdresValidator().ValidateAsync(model);
            if (!validation.IsValid)
            {
                return ValidationProblem(validation.ToString());
            }

            var id = await Service.CreateAdres(model);
            return CreatedAtAction(nameof(Get), new {id});
        }

        [HttpPut]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> PutAdres(AdresModel model)
        {
            var validation = await new AdresValidator().ValidateAsync(model);
            if (!validation.IsValid)
            {
                return ValidationProblem(validation.ToString());
            }

            if (await Service.UpdateAdres(model))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }

        [HttpDelete("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult> DeleteAdres(int id)
        {
            if (await Service.DeleteAdresById(id))
            {
                return Ok();
            }
            else
            {
                return NotFound();
            }
        }
    }
}