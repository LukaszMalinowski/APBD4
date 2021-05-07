using cwiczenia4_zen_s19743.Models.DTOs;

namespace cwiczenia4_zen_s19743.Services
{
    public interface IWarehouseService
    {
        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto);
    }
}