using cwiczenia4_zen_s19743.Models.DTOs;
using cwiczenia4_zen_s19743.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia4_zen_s19743.Controllers
{
    [Route("api/warehouses2")]
    [ApiController]
    public class Warehouse2Controller : ControllerBase
    {
        private readonly IWarehouseService _service;

        public Warehouse2Controller(Startup.ServiceResolver resolver)
        {
            _service = resolver("proc");
        }

        [HttpPost]
        public IActionResult RegisterProductAtWarehouse([FromBody] WarehouseDTO warehouseDto)
        {
            _service.RegisterProductAtWarehouse(warehouseDto);

            return Ok(0);
        }
    }
}