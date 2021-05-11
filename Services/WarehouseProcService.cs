using System;
using System.Data;
using System.Data.SqlClient;
using cwiczenia4_zen_s19743.Models.DTOs;
using Microsoft.Extensions.Configuration;

namespace cwiczenia4_zen_s19743.Services
{
    public class WarehouseProcService : IWarehouseService
    {
        private readonly IConfiguration _configuration;

        public WarehouseProcService(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        
        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "AddProductToWarehouse"
            };

            command.CommandType = CommandType.StoredProcedure;

            command.Parameters.AddWithValue("@IdProduct", warehouseDto.IdProduct);
            command.Parameters.AddWithValue("@IdWarehouse", warehouseDto.IdWarehouse);
            command.Parameters.AddWithValue("@Amount", warehouseDto.Amount);
            command.Parameters.AddWithValue("@CreatedAt", warehouseDto.CreatedAt);
            
            connection.Open();

            command.ExecuteNonQuery();

            connection.Close();

            return 0;
        }
    }
}