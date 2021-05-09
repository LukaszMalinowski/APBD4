using System;
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

        public WarehouseController(IWarehouseService service)
        {
            _service = service;
        }

        [HttpPost]
        public IActionResult RegisterProductAtWarehouse([FromBody] WarehouseDTO warehouseDto)
        {
            int key = 0;
            try
            {
               key = _service.RegisterProductAtWarehouse(warehouseDto);
            }
            catch (Exception e)
            {
                return NotFound(e.Message);
            }

            return Ok();
        }
    }
}