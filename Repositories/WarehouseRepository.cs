using System;
using cwiczenia4_zen_s19743.Models.DTOs;
using System.Data.SqlClient;
using cwiczenia4_zen_s19743.Exceptions;
using cwiczenia4_zen_s19743.Models;
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
            throw new NotImplementedException();
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

            connection.Close();

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

            connection.Close();

            return warehouseCount > 0;
        }

        public int GetOrderId(int? idProduct, int? amount, DateTime? createdAt)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount"
            };

            command.Parameters.AddWithValue("@IdProduct", idProduct);
            command.Parameters.AddWithValue("@Amount", amount);

            connection.Open();
            SqlDataReader dataReader = command.ExecuteReader();

            Order order = null;

            while (dataReader.Read())
            {
                order = new Order
                {
                    IdOrder = Convert.ToInt32(dataReader["IdOrder"].ToString()),
                    Amount = Convert.ToInt32(dataReader["Amount"].ToString()),
                    CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"].ToString()),
                    FulfilledAt = FulfilledAtOrNull(dataReader["FulfilledAt"]),
                    IdProduct = Convert.ToInt32(dataReader["IdProduct"].ToString())
                };
            }
            
            connection.Close();

            if (order == null)
                throw new OrderDoesntExistException("There's no order for this product");

            if (order.CreatedAt > createdAt)
                throw new OrderCreatedAtException(createdAt);

            return order.IdOrder;
        }

        public bool IsOrderCompleted(int idOrder)
        {
            using SqlConnection connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            SqlCommand command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder"
            };

            command.Parameters.AddWithValue("@IdOrder", idOrder);
            
            connection.Open();

            int orderCount = (int) command.ExecuteScalar();

            connection.Close();

            return orderCount != 0;
        }

        private DateTime? FulfilledAtOrNull(object fulfilledAt)
        {
            if (fulfilledAt == null)
                return null;

            if (fulfilledAt.ToString().Equals(""))
                return null;

            return Convert.ToDateTime(fulfilledAt.ToString());
        }
    }
}