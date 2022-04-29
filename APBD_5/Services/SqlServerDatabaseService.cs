using APBD_5.Models;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace APBD_5.Services
{
    public class SqlServerDatabaseService : IDatabaseService
    {

        private IConfiguration _configuration;

        public SqlServerDatabaseService(IConfiguration configuration)
        {
            _configuration = configuration;
        }


        public async Task<int> RegisterProductAsync(ProductWarehouse productWarehouse)
        {
            int res = 0;
            using var connection = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            using var command = new SqlCommand();
            command.Connection = connection;
            await connection.OpenAsync();
            var tran = await connection.BeginTransactionAsync();

            command.Transaction = (SqlTransaction)tran;

            try
            {
                command.CommandText = "SELECT COUNT(*) FROM Product WHERE IdProduct = @idProduct";
                command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
                int productCount = (int)await command.ExecuteScalarAsync();
                
               
                if (productCount == 0)
                {
                    return -1;
                }

                command.Parameters.Clear();
                command.CommandText = "SELECT COUNT(*) FROM Warehouse WHERE IdWarehouse = @idWarehouse";
                command.Parameters.AddWithValue("@idWarehouse", productWarehouse.IdWarehouse);
                int warehouseCount = (int)await command.ExecuteScalarAsync();
                if (warehouseCount <= 0)
                {
                    return -2;
                }

                

                command.Parameters.Clear();
                command.CommandText = "SELECT TOP 1 o.IdOrder  FROM \"Order\" o LEFT JOIN Product_Warehouse pw ON o.IdOrder = pw.IdOrder " +
                    "WHERE o.IdProduct = @idProduct AND o.Amount = @amount AND pw.IdProductWarehouse IS NULL AND o.CreatedAt < @createdAt";
                command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
                command.Parameters.AddWithValue("@amount", productWarehouse.Amount);
                command.Parameters.AddWithValue("@createdAt", productWarehouse.CreatedAt);
                
                int idOrder=-1;
                
                using (var dr = await command.ExecuteReaderAsync())
                {
                    if (await dr.ReadAsync())
                        idOrder = (int)dr["IdOrder"];
                   
                }

                if (idOrder == -1)
                    return -3;

                double price;
                command.CommandText = "SELECT Price FROM Product WHERE IdProduct = @IdProduct";
                using (var dr = await command.ExecuteReaderAsync())
                {
                    price = (double)dr["Price"];

                }
                command.Parameters.Clear();
                command.CommandText = "UPDATE \"Order\" SET FulfilledAt = GETDATE() WHERE IdOrder = @idOrder;";
                command.Parameters.AddWithValue("@idOrder", idOrder);
                await command.ExecuteNonQueryAsync();



                command.Parameters.Clear();
                command.CommandText = "INSERT INTO Product_Warehouse(IdWarehouse, IdProduct, IdOrder, Amount, Price, CreatedAt) VALUES(@idWarehouse, @idProduct, @idOrder, @amount, @amount*@price, @createdAt)";
                command.Parameters.AddWithValue("@idProduct", productWarehouse.IdProduct);
                command.Parameters.AddWithValue("@amount", productWarehouse.Amount);
                command.Parameters.AddWithValue("@createdAt", productWarehouse.CreatedAt);
                command.Parameters.AddWithValue("@idWarehouse", productWarehouse.IdWarehouse);
                command.Parameters.AddWithValue("@idOrder", idOrder);
                command.Parameters.AddWithValue("@price", price);
                await command.ExecuteNonQueryAsync();

                command.Parameters.Clear();
                command.CommandText = "SELECT TOP 1 IdProductWarehouse FROM Product_Warehouse ORDER BY ID DESC";
                int pdId = (int)await command.ExecuteScalarAsync();
                tran.Commit();
                return pdId;
            }
            catch(SqlException exc)
            {
                await tran.RollbackAsync();
            }
            catch(Exception exc)
            {
                await tran.RollbackAsync();
            }

            return res;
        }

        public async Task<int> RegisterProductByProcedureAsync(ProductWarehouse productWarehouse)
        {
            using var con = new SqlConnection(_configuration.GetConnectionString("ProductionDb"));
            using var com = new SqlCommand("AddProductToWarehouse", con);
            com.CommandType = CommandType.StoredProcedure;
            com.Parameters.AddWithValue("@IdProduct", productWarehouse.IdProduct);
            com.Parameters.AddWithValue("@IdWarehouse", productWarehouse.IdWarehouse);
            com.Parameters.AddWithValue("@Amount", productWarehouse.Amount);
            com.Parameters.AddWithValue("@CreatedAt", productWarehouse.CreatedAt);

            await con.OpenAsync();
            await com.ExecuteNonQueryAsync();
            

            return 1;
        }


    }
}
