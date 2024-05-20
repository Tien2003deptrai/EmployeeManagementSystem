using BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Runtime.Remoting.Contexts;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace EmployeeManagementSystem
{
    public partial class Salary : Form
    {
        private readonly EmployeeSalaryService _service;

        public Salary()
        {
            InitializeComponent();
            _service = new EmployeeSalaryService();

            DisplayEmployees();
            DisableFields();
            DisableButton();
        }

        public void DisableFields()
        {
            txtEmployeeID.Enabled = false;
            txtName.Enabled = false;
            txtPosition.Enabled = false;
        }

        public void DisableButton()
        {
            btnSalary.Enabled = false;
            btnSalary.BackColor = Color.White;
        }

        public void DisplayEmployees()
        {
            dataGridView1.DataSource = _service.GetEmployeeSalaries();
        }

        private void btn_Update_Click(object sender, EventArgs e)
        {
            string employeeID = txtEmployeeID.Text.Trim();
            int newSalary;

            if (int.TryParse(txtSalary.Text.Trim(), out newSalary))
            {
                _service.UpdateEmployeeSalary(employeeID, newSalary);
                DisplayEmployees();
                MessageBox.Show("Updated successfully!", "Information", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            else
            {
                MessageBox.Show("Please enter a valid salary!", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
        private void btnAddEmployee_Click(object sender, EventArgs e)
        {
            AddEmployee addEmployeeForm = new AddEmployee();
            addEmployeeForm.Show();
            this.Hide();
        }

        private void btn_Exit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtEmployeeID.Text = row.Cells[0].Value.ToString();
                txtName.Text = row.Cells[1].Value.ToString();
                txtPosition.Text = row.Cells[4].Value.ToString();
                txtSalary.Text = row.Cells[5].Value.ToString();
            }
        }

        private void btn_Clear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        public void ClearFields()
        {
            txtEmployeeID.Text = "";
            txtName.Text = "";
            txtPosition.Text = "";
            txtSalary.Text = "";
        }
    }
}
