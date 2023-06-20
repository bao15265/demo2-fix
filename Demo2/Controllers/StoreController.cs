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
                _storeService.AddStore(storeDto);
                return Ok();
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
                _storeService.UpdateStore(id, storeDto);
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
            _storeService.SearchStores(filter);
            return Ok();
        }

        [HttpGet("{storeId}/top-providers")]
        public IActionResult GetTopProviders(int storeId)
        {
            try
            {
                _storeService.GetTopProviders(storeId);
                return Ok();
            }
            catch (EntityNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
        }
    }
}
