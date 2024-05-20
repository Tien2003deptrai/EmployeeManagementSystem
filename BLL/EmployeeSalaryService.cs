using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL
{
    public class EmployeeSalaryService
    {
        private readonly EmployeeSalaryRepository _repository;

        public EmployeeSalaryService()
        {
            _repository = new EmployeeSalaryRepository();
        }

        public List<EmployeeSalary> GetEmployeeSalaries()
        {
            return _repository.GetEmployeeSalaries();
        }

        public void UpdateEmployeeSalary(string employeeID, int newSalary)
        {
            _repository.UpdateEmployeeSalary(employeeID, newSalary);
        }
    }
}
