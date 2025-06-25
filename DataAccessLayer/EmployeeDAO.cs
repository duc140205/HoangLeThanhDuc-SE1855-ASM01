using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BusinessObjects;
namespace DataAccessLayer
{
    public class EmployeeDAO
    {
        private static List<Employee> employees = new List<Employee>
    {
        new Employee
        {
            EmployeeId = 1,
            Name = "Hoang Duc",
            UserName = "admin",
            Password = "123",
            JobTitle = "Administrator"
        },
        new Employee
        {
            EmployeeId = 2,
            Name = "Nguyen Quoc Huy",
            UserName = "admin2",
            Password = "123",
            JobTitle = "Administrator 2"
        }
    };

        public static List<Employee> GetEmployees() => new List<Employee>(employees);

        public static Employee GetEmployeeById(int employeeId) =>
            employees.FirstOrDefault(e => e.EmployeeId == employeeId);

        public static void AddEmployee(Employee employee) => employees.Add(employee);

        public static void UpdateEmployee(Employee employee)
        {
            var existingEmployee = employees.FirstOrDefault(e => e.EmployeeId == employee.EmployeeId);
            if (existingEmployee != null)
            {
                existingEmployee.Name = employee.Name;
                existingEmployee.UserName = employee.UserName;
                existingEmployee.Password = employee.Password;
                existingEmployee.JobTitle = employee.JobTitle;
            }
        }

        public static void DeleteEmployee(int employeeId)
        {
            var employeeToRemove = employees.FirstOrDefault(e => e.EmployeeId == employeeId);
            if (employeeToRemove != null)
            {
                employees.Remove(employeeToRemove);
            }
        }

        public static Employee Login(string userName, string password) =>
            employees.FirstOrDefault(e => e.UserName == userName && e.Password == password);

    }
}
