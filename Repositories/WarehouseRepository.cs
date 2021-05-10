using System;
using System.Data.SqlClient;
using cwiczenia4_zen_s19743.Exceptions;
using cwiczenia4_zen_s19743.Models;
using cwiczenia4_zen_s19743.Models.DTOs;
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

        public bool ProductExists(int? idProduct)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT COUNT(*) FROM Product WHERE IdProduct = @IdProduct"
            };

            command.Parameters.AddWithValue("@IdProduct", idProduct);

            connection.Open();

            var productCount = (int) command.ExecuteScalar();

            connection.Close();

            return productCount > 0;
        }

        public bool WarehouseExists(int? idWarehouse)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @IdWarehouse"
            };

            command.Parameters.AddWithValue("@IdWarehouse", idWarehouse);

            connection.Open();

            var warehouseCount = (int) command.ExecuteScalar();

            connection.Close();

            return warehouseCount > 0;
        }

        public int GetOrderId(int? idProduct, int? amount, DateTime? createdAt)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT * FROM [Order] WHERE IdProduct = @IdProduct AND Amount = @Amount"
            };

            command.Parameters.AddWithValue("@IdProduct", idProduct);
            command.Parameters.AddWithValue("@Amount", amount);

            connection.Open();
            var dataReader = command.ExecuteReader();

            Order order = null;

            while (dataReader.Read())
                order = new Order
                {
                    IdOrder = Convert.ToInt32(dataReader["IdOrder"].ToString()),
                    Amount = Convert.ToInt32(dataReader["Amount"].ToString()),
                    CreatedAt = Convert.ToDateTime(dataReader["CreatedAt"].ToString()),
                    FulfilledAt = FulfilledAtOrNull(dataReader["FulfilledAt"]),
                    IdProduct = Convert.ToInt32(dataReader["IdProduct"].ToString())
                };

            connection.Close();

            if (order == null)
                throw new OrderDoesntExistException("There's no order for this product");

            if (order.CreatedAt > createdAt)
                throw new OrderCreatedAtException(createdAt);

            return order.IdOrder;
        }

        public bool IsOrderCompleted(int idOrder)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT COUNT(*) FROM Product_Warehouse WHERE IdOrder = @IdOrder"
            };

            command.Parameters.AddWithValue("@IdOrder", idOrder);

            connection.Open();

            var orderCount = (int) command.ExecuteScalar();

            connection.Close();

            return orderCount != 0;
        }

        public int RegisterProductAtWarehouse(WarehouseDTO warehouseDto, int orderId)
        {
            UpdateFulfilledAtInOrderTable(orderId);

            var productPrice = GetProductPrice(warehouseDto.IdProduct);

            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT MAX(IdProductWarehouse) AS maxId FROM Product_Warehouse"
            };

            connection.Open();

            var sqlDataReader = command.ExecuteReader();

            sqlDataReader.Read();

            var maxId = 0;

            if (!sqlDataReader["maxId"].ToString().Equals(""))
                maxId = Convert.ToInt32(sqlDataReader["maxId"].ToString());

            sqlDataReader.Close();

            command.CommandText = "SET IDENTITY_INSERT Product_Warehouse ON; " +
                                  "INSERT INTO Product_Warehouse (IdProductWarehouse, IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES (@IdProductWarehouse, @IdWarehouse, @IdProduct, @IdOrder, @Amount, @Price, @CreatedAt);SET IDENTITY_INSERT Product_Warehouse OFF; " +
                                  "SET IDENTITY_INSERT Product_Warehouse OFF;";

            command.Parameters.AddWithValue("@IdProductWarehouse", maxId + 1);
            command.Parameters.AddWithValue("@IdWarehouse", warehouseDto.IdWarehouse);
            command.Parameters.AddWithValue("@IdProduct", warehouseDto.IdProduct);
            command.Parameters.AddWithValue("@IdOrder", orderId);
            command.Parameters.AddWithValue("@Amount", warehouseDto.Amount);
            command.Parameters.AddWithValue("@Price", productPrice * warehouseDto.Amount);
            command.Parameters.AddWithValue("@CreatedAt", DateTime.Now);

            command.ExecuteNonQuery();

            return maxId + 1;
        }

        private decimal GetProductPrice(int? idProduct)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct"
            };

            command.Parameters.AddWithValue("@IdProduct", idProduct);

            connection.Open();

            return Convert.ToDecimal(command.ExecuteScalar());
        }

        private void UpdateFulfilledAtInOrderTable(int orderId)
        {
            using var connection = new SqlConnection(_configuration.GetConnectionString("PjatkDb"));
            var command = new SqlCommand
            {
                Connection = connection,
                CommandText = "UPDATE [Order] SET FulfilledAt = @CurrentDate WHERE IdOrder = @IdOrder"
            };

            command.Parameters.AddWithValue("@IdOrder", orderId);
            command.Parameters.AddWithValue("@CurrentDate", DateTime.Now);

            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
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