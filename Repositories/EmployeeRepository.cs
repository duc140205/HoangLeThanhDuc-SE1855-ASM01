using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
using DataAccessLayer;

namespace Repositories
{
    public class EmployeeRepository : IEmployeeRepository
    {
        public void AddEmployee(Employee employee)
        {
            EmployeeDAO.AddEmployee(employee);
        }

        public void DeleteEmployee(int employeeId)
        {
            EmployeeDAO.DeleteEmployee(employeeId);
        }

        public Employee GetEmployeeById(int employeeId)
        {
            return EmployeeDAO.GetEmployeeById(employeeId);
        }

        public List<Employee> GetEmployees()
        {
            return EmployeeDAO.GetEmployees();
        }

        public Employee Login(string userName, string password)
        {
            return EmployeeDAO.Login(userName, password);
        }

        public void UpdateEmployee(Employee employee)
        {
            EmployeeDAO.UpdateEmployee(employee);
        }
    }
}
