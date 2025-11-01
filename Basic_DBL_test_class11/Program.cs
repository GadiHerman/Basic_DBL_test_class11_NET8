using System.Threading.Tasks;

namespace Basic_DBL_test_class11
{
    internal class Program
    {
        static async Task Main(string[] args)
        {
            //CustomerDB customerDB = new CustomerDB();
            //List<Customer> list = await customerDB.SelectAllAsync();

            //Customer customer = new Customer();
            //customer.Name = "Tal";
            //customer.Email = "tal@gmail.com";
            //customer.IsAdmin = false;
            //int id = await customerDB.InsertAsync(customer, "1234");
            //Console.WriteLine();


            NewCustomerDB customerDB = new NewCustomerDB();
         

            Customer customer = new Customer();
            customer.Name = "Tal";
            customer.Email = "tal@gmail.com";
            customer.IsAdmin = false;
            int id = await customerDB.InsertAsync(customer, "1234");
            Console.WriteLine(id);
        }
    }
}
