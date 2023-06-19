using Demo2.Entities;
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
        public ActionResult<Provider> AddProvider(Provider provider)
        {
            var addedProvider = _providerService.AddProvider(provider);
            return Ok(addedProvider);
        }

        [HttpPut("{providerId}")]
        public ActionResult<Provider> UpdateProvider(int providerId, Provider provider)
        {
            if (providerId != provider.Id)
                return BadRequest("Provider ID mismatch.");

            var updatedProvider = _providerService.UpdateProvider(provider);
            return Ok(updatedProvider);
        }

        [HttpDelete("{providerId}")]
        public IActionResult DeleteProvider(int providerId)
        {
            _providerService.DeleteProvider(providerId);
            return NoContent();
        }
    }

}
