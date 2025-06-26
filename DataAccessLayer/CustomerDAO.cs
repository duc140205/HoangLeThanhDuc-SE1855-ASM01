using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
namespace DataAccessLayer
{
    public class CustomerDAO
    {
        public static List<Customer> customers = new List<Customer>
        {
            new Customer(1, "Tech Solutions", "Alice Smith", "IT Manager", "123 Tech Lane", 0987654321),
            new Customer(2, "Book Haven", "Bob Johnson", "Sales Manager", "456 Book St", 0934923124),
            new Customer(3, "Fashion Hub", "Charlie Brown", "Marketing Director", "789 Fashion Ave", 0934923125),
            new Customer(4, "Sports World", "Diana Prince", "Operations Manager", "321 Sports Blvd", 0934923126),
            new Customer(5, "Toy Kingdom", "Ethan Hunt", "Product Manager", "654 Toy Rd", 0934923127)
        };

        // CRUD
        public static List<Customer> GetCustomers() => new List<Customer>(customers);

        public static Customer GetCustomerById(int customerId) =>
            customers.FirstOrDefault(c => c.CustomerId == customerId);

        public static void AddCustomer(Customer customer)
        {

            // Tìm ID lớn nhất hiện có trong danh sách
            // Nếu danh sách rỗng, ID lớn nhất sẽ là 0
            int maxId = customers.Count > 0 ? customers.Max(c => c.CustomerId) : 0;

            // Tạo ID mới bằng cách lấy ID lớn nhất + 1
            int newId = maxId + 1;

            // Gán ID mới này cho customer sắp được thêm vào
            customer.CustomerId = newId;
            customers.Add(customer);
        }

        public static void UpdateCustomer(Customer customer)
        {
            var existingCustomer = customers.FirstOrDefault(c => c.CustomerId == customer.CustomerId);
            if (existingCustomer != null)
            {
                existingCustomer.CompanyName = customer.CompanyName;
                existingCustomer.ContactName = customer.ContactName;
                existingCustomer.ContactTitle = customer.ContactTitle;
                existingCustomer.Address = customer.Address;
                existingCustomer.Phone = customer.Phone;
            }
        }

        public static void DeleteCustomer(int customerId)
        {
            var customerToRemove = customers.FirstOrDefault(c => c.CustomerId == customerId);
            if (customerToRemove != null)
            {
                customers.Remove(customerToRemove);
            }
        }
        public static List<Customer> SearchCustomers(string searchTerm)
        {
            if (string.IsNullOrWhiteSpace(searchTerm))
                return new List<Customer>(customers);
            return customers
                .Where(c => c.CompanyName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase) ||
                            c.ContactName.Contains(searchTerm, StringComparison.OrdinalIgnoreCase))
                .ToList();
        }

        public static Customer GetCustomerByPhone(int phone)
        {
            return customers.FirstOrDefault(c => c.Phone == phone);
        }

    }
}
