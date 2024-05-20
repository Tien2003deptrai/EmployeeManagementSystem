using BLL;
using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class RegisterForm : Form
    {

        private UserService _userService;

        public RegisterForm()
        {
            InitializeComponent();
            _userService = new UserService();
        }

        public void clearFields()
        {
            txtUsername.Text = "";
            txtPassword.Text = "";
        }

        private void btnRegister_Click(object sender, EventArgs e)
        {
            string username = txtUsername.Text.Trim();
            string password = txtPassword.Text.Trim();

            try
            {
                _userService.RegisterUser(username, password);
                MessageBox.Show("User registered successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                clearFields();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Error: {ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
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

        private void exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
