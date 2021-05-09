using System;
using cwiczenia4_zen_s19743.Models.DTOs;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace cwiczenia4_zen_s19743.Repositories
{
    public class WarehouseRepository : IWarehouseRepository
    {
        private readonly IConfiguration _configuration;

        public WarehouseRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto)
        {
            throw new System.NotImplementedException();
        }

        public bool ProductExists(int? idProduct)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct"
            };

            command.Parameters.AddWithValue("@IdProduct", idProduct);
            
            connection.Open();

            int productCount = (int) command.ExecuteScalar();

            return productCount > 0;
        }

        public bool WarehouseExists(int? idWarehouse)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse"
            };

            command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);
            
            connection.Open();

            int warehouseCount = (int) command.ExecuteScalar();

            return warehouseCount > 0;
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