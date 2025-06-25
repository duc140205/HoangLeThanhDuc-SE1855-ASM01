using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using Repositories;

namespace Services
{
    public class EmployeeService : IEmployeeService
    {
        private readonly IEmployeeRepository iemployeeRepository;
        public EmployeeService()
        {
            iemployeeRepository = new EmployeeRepository(); // Replace with actual repository instantiation
        }    

        public List<Employee> GetEmployees()
        {
            return iemployeeRepository.GetEmployees();
        }

        Employee IEmployeeService.GetEmployeeById(int employeeId)
        {
            return iemployeeRepository.GetEmployeeById(employeeId);
        }

        public void AddEmployee(Employee employee)
        {
            iemployeeRepository.AddEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            iemployeeRepository.UpdateEmployee(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            iemployeeRepository.DeleteEmployee(employeeId);
        }

        public Employee Login(string userName, string password)
        {
            return iemployeeRepository.Login(userName, password);
        }
    }
}
