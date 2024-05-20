using DTO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class EmployeeSalaryRepository
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\employee.mdf;Integrated Security=True;Connect Timeout=30";

        public List<EmployeeSalary> GetEmployeeSalaries()
        {
            List<EmployeeSalary> salaries = new List<EmployeeSalary>();

            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();

                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, db))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            EmployeeSalary salary = new EmployeeSalary
                            {
                                EmployeeID = reader["employee_id"].ToString(),
                                Name = reader["full_name"].ToString(),
                                Gender = reader["gender"].ToString(),
                                Contact = reader["contact_number"].ToString(),
                                Position = reader["position"].ToString(),
                                Salary = (int)reader["salary"]
                            };

                            salaries.Add(salary);
                        }
                    }
                }
            }

            return salaries;
        }

        public void UpdateEmployeeSalary(string employeeID, int newSalary)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                if (db.State != ConnectionState.Open)
                {
                    db.Open();

                    string updateData = "UPDATE employees SET salary = @salary" +
                        " WHERE employee_id = @employeeID";

                    using (SqlCommand cmd = new SqlCommand(updateData, db))
                    {
                        cmd.Parameters.AddWithValue("@salary", newSalary);
                        cmd.Parameters.AddWithValue("@employeeID", employeeID);

                        cmd.ExecuteNonQuery();
                    }
                }
            }
        }
    }
}
