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
using BLL;

namespace EmployeeManagementSystem
{
    public partial class LoginForm : Form
    {
        private UserService _userService;

        public LoginForm() 
        {
            InitializeComponent();
            _userService = new UserService();
        }

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                if (_userService.Login(username, password))
                {
                    MessageBox.Show("Login successful!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    AddEmployee employeeForm = new AddEmployee();
                    employeeForm.Show();
                    this.Hide();
                }
                else
                {
                    MessageBox.Show("Incorrect username/password", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void checkShowPass_CheckedChanged(object sender, EventArgs e)
        {
            txtPassword.PasswordChar = checkShowPass.Checked ? '\0' : '*';
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            RegisterForm registerForm = new RegisterForm();
            registerForm.Show();
            this.Hide();
        }
    }
}
