using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data;
using System.Data.SqlClient;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        SqlConnection db  = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\employee.mdf;Integrated Security=True;Connect Timeout=30");
        public LoginForm()
        {
            InitializeComponent();
        }

        public void showError(string message)
        {
            MessageBox.Show(message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void showSuccess(string message)
        {
            MessageBox.Show(message, "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void showException(Exception ex)
        {
            MessageBox.Show("Error" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = checkShowPass.Checked ? '\0' : '*';
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if (txtUsername.Text == "" || txtPassword.Text == "")
            {
                showError("Please all blank field");
                return;
            }
            else
            {
                if(db.State != ConnectionState.Open)
                {
                    try
                    {
                        db.Open();

                        string sql = "SELECT * FROM Users WHERE username = @username and password = @password";
                        using (SqlCommand conn = new SqlCommand(sql, db))
                        {
                            conn.Parameters.AddWithValue("@username", txtUsername.Text);
                            conn.Parameters.AddWithValue("@password", txtPassword.Text);

                            SqlDataAdapter adpter = new SqlDataAdapter(conn);
                            DataTable table = new DataTable();
                            adpter.Fill(table);

                            if(table.Rows.Count >= 1)
                            {
                                showSuccess("Login successfuly !");

                                AddEmployee employeeForm = new AddEmployee();
                                employeeForm.Show();
                                this.Hide();
                            }
                            else
                            {
                                showError("Incorrect username/password");
                            }
                        }
                    }   
                    catch (Exception ex) 
                    {
                        showException(ex);
                    }
                    finally { db.Close(); }
                }

            }

        }
    }
}
