using System;
using cwiczenia4_zen_s19743.Models.DTOs;

namespace cwiczenia4_zen_s19743.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto)
        {
            throw new System.NotImplementedException();
        }

        public bool ProductExists(int? idProduct)
        {
            throw new System.NotImplementedException();
        }

        public bool WarehouseExists(int? idWarehouse)
        {
            throw new System.NotImplementedException();
        }

        public int GetOrderId(int? idProduct, int? amount, DateTime? createdAt)
        {
            throw new NotImplementedException();
        }

        public bool IsOrderCompleted(int idOrder)
        {
            throw new NotImplementedException();
        }
    }
}