using System;
using cwiczenia4_zen_s19743.Models.DTOs;

namespace cwiczenia4_zen_s19743.Repositories
{
    public interface IWarehouseRepository
    {
        public bool ProductExists(int? idProduct);

        public bool WarehouseExists(int? idWarehouse);

        public int GetOrderId(int? idProduct, int? amount, DateTime? createdAt);

        public bool IsOrderCompleted(int idOrder);
        
        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto, int orderId);
    }
}