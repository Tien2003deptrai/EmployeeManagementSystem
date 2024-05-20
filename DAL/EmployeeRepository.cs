using DTO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;

namespace DAL
{
    public class EmployeeRepository
    {
        private readonly string _connectionString = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\employee.mdf;Integrated Security=True;Connect Timeout=30";

        public List<Employee> GetEmployees()
        {
            List<Employee> employees = new List<Employee>();
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";
                using (SqlCommand cmd = new SqlCommand(selectData, db))
                {
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        Employee employee = new Employee
                        {
                            ID = (int)reader["id"],
                            EmployeeID = reader["employee_id"].ToString(),
                            Name = reader["full_name"].ToString(),
                            Gender = reader["gender"].ToString(),
                            Contact = reader["contact_number"].ToString(),
                            Position = reader["position"].ToString(),
                            Image = reader["image"].ToString(),
                            Salary = (int)reader["salary"],
                            Status = reader["status"].ToString()
                        };
                        employees.Add(employee);
                    }
                }
            }
            return employees;
        }

        public void AddEmployee(Employee employee)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string insertSql = "INSERT INTO employees (employee_id, full_name, gender, contact_number, position, image, salary, insert_date, status) VALUES (@employeeID, @fullname, @gender, @contactNumber, @position, @image, @salary, @insertDate, @status)";
                using (SqlCommand cmd = new SqlCommand(insertSql, db))
                {
                    cmd.Parameters.AddWithValue("@employeeID", employee.EmployeeID);
                    cmd.Parameters.AddWithValue("@fullname", employee.Name);
                    cmd.Parameters.AddWithValue("@gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@contactNumber", employee.Contact);
                    cmd.Parameters.AddWithValue("@position", employee.Position);
                    cmd.Parameters.AddWithValue("@image", employee.Image);
                    cmd.Parameters.AddWithValue("@salary", employee.Salary);
                    cmd.Parameters.AddWithValue("@insertDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@status", employee.Status);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void UpdateEmployee(Employee employee)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string updateSql = "UPDATE employees SET full_name = @fullName, gender = @gender, contact_number = @contactNum, position = @position, update_date = @updateDate, status = @status WHERE employee_id = @employeeID";
                using (SqlCommand cmd = new SqlCommand(updateSql, db))
                {
                    cmd.Parameters.AddWithValue("@fullName", employee.Name);
                    cmd.Parameters.AddWithValue("@gender", employee.Gender);
                    cmd.Parameters.AddWithValue("@contactNum", employee.Contact);
                    cmd.Parameters.AddWithValue("@position", employee.Position);
                    cmd.Parameters.AddWithValue("@updateDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@status", employee.Status);
                    cmd.Parameters.AddWithValue("@employeeID", employee.EmployeeID);
                    cmd.ExecuteNonQuery();
                }
            }
        }

        public void DeleteEmployee(string employeeId)
        {
            using (SqlConnection db = new SqlConnection(_connectionString))
            {
                db.Open();
                string deleteSql = "UPDATE employees SET delete_date = @deleteDate WHERE employee_id = @employeeID";
                using (SqlCommand cmd = new SqlCommand(deleteSql, db))
                {
                    cmd.Parameters.AddWithValue("@deleteDate", DateTime.Today);
                    cmd.Parameters.AddWithValue("@employeeID", employeeId);
                    cmd.ExecuteNonQuery();
                }
            }
        }
    }
}
