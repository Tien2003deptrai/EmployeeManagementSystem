using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EmployeeManagementSystem
{
    class EmployeeData
    {
        public int ID { get; set; }

        public string EmployeeID {  get; set; }

        public string Name { get; set; }

        public string Gender { get; set; }
        
        public string Contact { get; set; }
        
        public string Position { get; set; }
        
        public string Image { get; set; }
        
        public int Salary { get; set; }
        
        public string Status { get; set; }

        SqlConnection db = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\employee.mdf;Integrated Security=True;Connect Timeout=30");

        public List<EmployeeData> employeeListData()
        {
            List<EmployeeData> listData = new List<EmployeeData>();

            if (db.State != ConnectionState.Open)
            {
                try
                {
                    db.Open();

                    string selectData = "SELECT * FROM employees WHERE delete_date IS NULL";

                    using (SqlCommand cmd = new SqlCommand(selectData, db))
                    {
                        SqlDataReader reader = cmd.ExecuteReader();

                        while(reader.Read())
                        {
                            EmployeeData ed = new EmployeeData();
                            ed.ID = (int)reader["id"];
                            ed.EmployeeID = reader["employee_id"].ToString();
                            ed.Name = reader["full_name"].ToString();
                            ed.Gender = reader["gender"].ToString();
                            ed.Contact = reader["contact_number"].ToString();
                            ed.Position = reader["position"].ToString();
                            ed.Image = reader["image"].ToString();
                            ed.Salary = (int)reader["salary"];
                            ed.Status = reader["status"].ToString();

                            listData.Add(ed);
                        }
                    }
                }
                catch (Exception ex)
                {

                    Console.WriteLine("Error: " + ex);
                }
                finally { db.Close(); }

            }
            return listData;
        }
    }
}
