using System.Threading.Tasks;

namespace Basic_DBL_test_class11
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            CustomerDB customerDB = new CustomerDB();

            Console.WriteLine("--- Inserting Customer ---");
            Customer customer = new Customer();
            customer.Name = "Tal_New";
            customer.Email = "tal_new@gmail.com";
            customer.IsAdmin = false;
            int affectedInsert = await customerDB.InsertAsync(customer, "1234");
            Console.WriteLine($"Inserted {affectedInsert} row(s).");

            Console.WriteLine("\n--- Selecting All Customers ---");
            List<Customer> list = await customerDB.SelectAllAsync();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Id: {list[i].Id}, Name: {list[i].Name}, Email: {list[i].Email}");
            }

            if (list.Count > 0)
            {
                Console.WriteLine("\n--- Updating Customer ---");
                Customer toUpdate = list[list.Count - 1];
                toUpdate.Name = "Tal_Updated";
                toUpdate.Email = "tal_updated@gmail.com";

                int affectedUpdate = await customerDB.UpdateAsync(toUpdate);
                Console.WriteLine($"Updated {affectedUpdate} row(s).");
            }

            Console.WriteLine("\n--- Selecting All After Update ---");
            list = await customerDB.SelectAllAsync();
            for (int i = 0; i < list.Count; i++)
            {
                Console.WriteLine($"Id: {list[i].Id}, Name: {list[i].Name}, Email: {list[i].Email}");
            }

            if (list.Count > 0)
            {
                Console.WriteLine("\n--- Deleting Customer ---");
                int idToDelete = list[list.Count - 1].Id;
                int affectedDelete = await customerDB.DeleteAsync(idToDelete);
                Console.WriteLine($"Deleted {affectedDelete} row(s).");
            }
        }
    }
}
