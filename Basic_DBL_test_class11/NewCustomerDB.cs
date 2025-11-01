using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Basic_DBL_test_class11
{
    public class NewCustomerDB : BaseDB<Customer>
    {
        protected override string GetPrimaryKeyName()
        {
            return "CustomerID";
        }

        protected override string GetTableName()
        {
            return "customers";
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
    }
}
