using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class CustomerRepository : ICustomerRepository
    {
        public void AddCustomer(Customer customer)
        {
            CustomerDAO.AddCustomer(customer);
        }

        public void DeleteCustomer(int customerId)
        {
            CustomerDAO.DeleteCustomer(customerId);
        }

        public Customer GetCustomerById(int customerId)
        {
            return CustomerDAO.GetCustomerById(customerId);
        }

        public List<Customer> GetCustomers()
        {
            return CustomerDAO.GetCustomers();
        }

        public List<Customer> SearchCustomers(string searchTerm)
        {
            return CustomerDAO.SearchCustomers(searchTerm);
        }

        public void UpdateCustomer(Customer customer)
        {
            CustomerDAO.UpdateCustomer(customer);
        }

        public Customer GetCustomerByPhone(string phone)
        {
            return CustomerDAO.GetCustomerByPhone(phone);
        }
    }
}
