using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class RegisterForm : Form
    {
        SqlConnection db = new SqlConnection(@"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\employee.mdf;Integrated Security=True;Connect Timeout=30");
        
        public void showError(string message)
        {
            MessageBox.Show(message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void showErrorException(Exception ex)
        {
            MessageBox.Show("Error" + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void showSuccess(string message)
        {
            MessageBox.Show(message, "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public RegisterForm()
        {
            InitializeComponent();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            LoginForm loginForm = new LoginForm();
            loginForm.Show();
            this.Hide();
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = checkShowPass.Checked ? '\0' : '*';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            if (txtPassword.Text == "" || txtPassword.Text == "") {
                showError("Please fill all blank field"); 
                return; 
            }
            else
            {
                if (db.State != ConnectionState.Open)
                {
                    try
                    {
                        db.Open();
                        string selectUsername = "SELECT COUNT(id) FROM Users WHERE username = @username";
                        using (SqlCommand checkUser = new SqlCommand(selectUsername, db))
                        {
                            checkUser.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                            int count = (int)checkUser.ExecuteScalar();
                            if(count >= 1)
                            {
                                MessageBox.Show(txtUsername.Text.Trim() + " is already taken"
                                    , "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                            }
                            else
                            {
                                DateTime today = DateTime.Today;
                                string sql = "INSERT INTO Users " +
                                             "(username, password, date_register)" +
                                             "VALUES (@username, @password, @date_register)";
                                using (SqlCommand conn = new SqlCommand(sql, db))
                                {
                                    conn.Parameters.AddWithValue("@username", txtUsername.Text.Trim());
                                    conn.Parameters.AddWithValue("@password", txtPassword.Text.Trim());
                                    conn.Parameters.AddWithValue("@date_register", today);

                                    conn.ExecuteNonQuery();

                                    showSuccess("Successfully Register !");

                                    LoginForm loginForm = new LoginForm();
                                    loginForm.Show();
                                    this.Hide();
                                }
                            }
                        }
                        
                    } catch(Exception ex)
                    {
                        showErrorException(ex);
                    }
                    finally { db.Close(); }
                }
            }
        }
    }
}
