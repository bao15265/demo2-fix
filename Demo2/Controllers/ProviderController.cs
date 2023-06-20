using Demo2.Dto.Provider;
using Demo2.Exceptions;
using Demo2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo2.Controllers
{
    [ApiController]
    [Route("api/providers")]
    public class ProviderController : ControllerBase
    {
        private readonly IProviderService _providerService;

        public ProviderController(IProviderService providerService)
        {
            _providerService = providerService;
        }

        [HttpPost]
        public IActionResult AddProvider([FromBody] ProviderDto providerDto)
        {
            try
            {
                _providerService.AddProvider(providerDto);
                return Ok();
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateProvider(int id, [FromBody] ProviderDto providerDto)
        {
            try
            {
                _providerService.UpdateProvider(id, providerDto);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteProvider(int id)
        {
            try
            {
                _providerService.DeleteProvider(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
