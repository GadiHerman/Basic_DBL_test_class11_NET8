using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_DBL_test_class11
{
    public class CustomerDB : DB
    {
        public async Task<List<Customer>> SelectAllAsync()
        {
            List<Customer> customers = new List<Customer>();

            await conn.OpenAsync();
            cmd.CommandText = "SELECT CustomerID, Name, Email, IsAdmin FROM Customers";
            reader = (MySql.Data.MySqlClient.MySqlDataReader)await cmd.ExecuteReaderAsync();
            while (await reader.ReadAsync())
            {
                Customer customer = new Customer();
                customer.Id = reader.GetInt32(0);
                customer.Name = reader.GetString(1);
                customer.Email = reader.GetString(2);
                customer.IsAdmin = reader.GetBoolean(3);
                customers.Add(customer);
            }
            await conn.CloseAsync();

            return customers;
        }
  
        public async Task<int> InsertAsync(Customer customer, string CustomerPassword)
        {
            await conn.OpenAsync();

            cmd.Parameters.Clear();
            cmd.CommandText = "INSERT INTO Customers (Name, Email,CustomerPassword, IsAdmin) VALUES (@name, @email,@customerPassword, @isAdmin)";
            cmd.Parameters.AddWithValue("@name", customer.Name);
            cmd.Parameters.AddWithValue("@email", customer.Email);
            cmd.Parameters.AddWithValue("@isAdmin", customer.IsAdmin);
            cmd.Parameters.AddWithValue("@customerPassword", CustomerPassword);

            await cmd.ExecuteNonQueryAsync();

            cmd.CommandText = "SELECT LAST_INSERT_ID()";
            object result = await cmd.ExecuteScalarAsync();
            int newId = Convert.ToInt32(result);

            await conn.CloseAsync();

            return newId;
        }
    }
}
