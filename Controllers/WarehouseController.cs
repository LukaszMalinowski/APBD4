using cwiczenia4_zen_s19743.Exceptions;
using cwiczenia4_zen_s19743.Models.DTOs;
using cwiczenia4_zen_s19743.Services;
using Microsoft.AspNetCore.Mvc;

namespace cwiczenia4_zen_s19743.Controllers
{
    [Route("api/warehouses")]
    [ApiController]
    public class WarehouseController : ControllerBase
    {
        private readonly IWarehouseService _service;

        public WarehouseController(Startup.ServiceResolver resolver)
        {
            _service = resolver("repo");
        }

        [HttpPost]
        public IActionResult RegisterProductAtWarehouse([FromBody] WarehouseDTO warehouseDto)
        {
            int id;

            try
            {
                id = _service.RegisterProductAtWarehouse(warehouseDto);
            }
            catch (BaseException e)
            {
                return NotFound(e.Message);
            }

            return Ok(id);
        }
    }
}