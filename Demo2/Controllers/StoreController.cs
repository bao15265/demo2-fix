using Demo2.Entities;
using Demo2.Filters;
using Demo2.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace Demo2.Controllers
{
    [ApiController]
    [Route("api/stores")]
    public class StoreController : ControllerBase
    {
        private readonly IStoreService _storeService;

        public StoreController(IStoreService storeService)
        {
            _storeService = storeService;
        }

        [HttpPost]
        public ActionResult<Store> AddStore(Store store)
        {
            var addedStore = _storeService.AddStore(store);
            return Ok(addedStore);
        }

        [HttpPut("{storeId}")]
        public ActionResult<Store> UpdateStore(int storeId, Store store)
        {
            if (storeId != store.Id)
                return BadRequest("Store ID mismatch.");

            var updatedStore = _storeService.UpdateStore(store);
            return Ok(updatedStore);
        }

        [HttpDelete("{storeId}")]
        public IActionResult DeleteStore(int storeId)
        {
            _storeService.DeleteStore(storeId);
            return NoContent();
        }

        [HttpGet]
        public ActionResult<List<Store>> GetStores([FromQuery] PaginationFilter filter, [FromQuery] string keyword)
        {
            var stores = _storeService.GetStores(filter, keyword);
            return Ok(stores);
        }

        [HttpGet("{storeId}/providers")]
        public ActionResult<List<Provider>> GetProvidersWithHighestIntimacy(int storeId)
        {
            var providers = _storeService.GetProvidersWithHighestIntimacy(storeId);
            return Ok(providers);
        }
    }

}
