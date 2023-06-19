using Demo2.Dto.Store;
using Demo2.Entities;
using Demo2.Exceptions;
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
        public IActionResult AddStore([FromBody] StoreDto storeDto)
        {
            try
            {
                var store = _storeService.AddStore(storeDto);
                return Ok(store);
            }
            catch (DuplicateEntityException ex)
            {
                return Conflict(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult UpdateStore(int id, [FromBody] StoreDto storeDto)
        {
            try
            {
                var store = _storeService.UpdateStore(id, storeDto);
                return Ok(store);
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
        public IActionResult DeleteStore(int id)
        {
            try
            {
                _storeService.DeleteStore(id);
                return NoContent();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }

        [HttpGet]
        [Route("search")]
        public IActionResult SearchStores([FromQuery] StoreFilterDto filter)
        {
            var stores = _storeService.SearchStores(filter);
            return Ok(stores);
        }

        [HttpGet("{storeId}/top-providers")]
        public IActionResult GetTopProviders(int storeId)
        {
            try
            {
                var providers = _storeService.GetTopProviders(storeId);
                return Ok(providers);
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
