using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class CustomerService : ICustomerService
    {
        private readonly ICustomerRepository icustomerRepository;
        public CustomerService()
        {
            // Assuming a concrete implementation of ICustomerRepository is available
            icustomerRepository = new CustomerRepository(); // Replace with actual repository instantiation
        }
        public void AddCustomer(Customer customer)
        {
            icustomerRepository.AddCustomer(customer);
        }
        public void DeleteCustomer(int customerId)
        {
            icustomerRepository.DeleteCustomer(customerId);
        }
        public List<Customer> GetCustomers()
        {
            return icustomerRepository.GetCustomers();
        }
        public Customer GetCustomerById(int customerId)
        {
            return icustomerRepository.GetCustomerById(customerId);
        }
        public List<Customer> SearchCustomers(string searchTerm)
        {
            return icustomerRepository.SearchCustomers(searchTerm);
        }
        public void UpdateCustomer(Customer customer)
        {
            icustomerRepository.UpdateCustomer(customer);
        }
        public Customer GetCustomerByPhone(int phone)
        {
            return icustomerRepository.GetCustomerByPhone(phone);
        }
    }
}
