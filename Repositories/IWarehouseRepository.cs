using cwiczenia4_zen_s19743.Models.DTOs;

namespace cwiczenia4_zen_s19743.Repositories
{
    public interface IWarehouseRepository
    {
        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto);

        public bool ProductExists(int? idProduct);

        public bool WarehouseExists(int? idWarehouse);
    }
}