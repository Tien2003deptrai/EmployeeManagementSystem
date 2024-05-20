using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;
using System.Runtime.Remoting.Contexts;

namespace EmployeeManagementSystem
{
    internal class SalaryData
    {
        public string EmployeeID { get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }

        public string Contact { get; set; }

        public string Position { get; set; }

        public int Salary { get; set; }

        SqlConnection db = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\employee.mdf;Integrated Security=True;Connect Timeout=30");

        public List<SalaryData> salaryEmployeeListData()
        {
            List<SalaryData> listdata = new List<SalaryData>();

            if (db.State != ConnectionState.Open)
            {
                try
                {
                    db.Open();

                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, db))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            SalaryData sd = new SalaryData();
                            sd.EmployeeID = reader["employee_id"].ToString();
                            sd.Name = reader["full_name"].ToString();
                            sd.Gender = reader["gender"].ToString();
                            sd.Contact = reader["contact_number"].ToString();
                            sd.Position = reader["position"].ToString();
                            sd.Salary = (int)reader["salary"];

                            listdata.Add(sd);
                        }
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: " + ex);
                }
                finally
                {
                    db.Close();
                }
            }
            return listdata;
        }

    }
}
