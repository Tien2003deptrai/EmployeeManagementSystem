using DAL;
using DTO;
using System;
using System.Collections.Generic;

namespace BLL
{
    public class EmployeeService
    {
        private readonly EmployeeRepository _employeeRepository;

        public EmployeeService()
        {
            _employeeRepository = new EmployeeRepository();
        }

        public List<Employee> GetEmployees()
        {
            return _employeeRepository.GetEmployees();
        }

        public void AddEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.EmployeeID))
                throw new ArgumentException("Employee ID is required.");
            if (string.IsNullOrWhiteSpace(employee.Name))
                throw new ArgumentException("Name is required.");
            if (string.IsNullOrWhiteSpace(employee.Contact))
                throw new ArgumentException("Contact number is required.");

            _employeeRepository.AddEmployee(employee);
        }

        public void UpdateEmployee(Employee employee)
        {
            if (string.IsNullOrWhiteSpace(employee.EmployeeID))
                throw new ArgumentException("Employee ID is required.");
            if (string.IsNullOrWhiteSpace(employee.Name))
                throw new ArgumentException("Name is required.");

            _employeeRepository.UpdateEmployee(employee);
        }

        public void DeleteEmployee(string employeeId)
        {
            if (string.IsNullOrWhiteSpace(employeeId))
                throw new ArgumentException("Employee ID is required.");

            _employeeRepository.DeleteEmployee(employeeId);
        }
    }
}
