using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_DBL_test_class11
{
    public class CustomerDB : BaseDB<Customer>
    {

        protected override string GetPrimaryKeyName()
        {
            return "CustomerID";
        }

        protected override string GetTableName()
        {
            return "customers";
        }
        protected override Customer CreateModel(object[] row)
        {
            Customer c = new Customer();
            c.Id = int.Parse(row[0].ToString());
            c.Name = row[1].ToString();
            c.Email = row[2].ToString();
            c.IsAdmin = bool.Parse(row[4].ToString());
            return c;
        }

        public async Task<int> InsertAsync(Customer customer, string CustomerPassword)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Name", customer.Name);
            fields.Add("Email", customer.Email);
            fields.Add("IsAdmin", customer.IsAdmin);
            fields.Add("CustomerPassword", CustomerPassword);
            return await InsertAsync(fields);
        }

        public async Task<int> UpdateAsync(Customer customer)
        {
            Dictionary<string, object> fields = new Dictionary<string, object>();
            fields.Add("Name", customer.Name);
            fields.Add("Email", customer.Email);
            fields.Add("IsAdmin", customer.IsAdmin);
            return await base.UpdateAsync(fields, customer.Id);
        }
    }
}
