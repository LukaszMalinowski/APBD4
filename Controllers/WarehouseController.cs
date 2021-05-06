using cwiczenia4_zen_s19743.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia4_zen_s19743.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult RegisterProductAtWarehouse()
        {
            return Ok();
        }
    }
}