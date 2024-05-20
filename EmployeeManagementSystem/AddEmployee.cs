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
using System.Messaging;
using System.Runtime.Remoting.Messaging;
using System.IO;
using System.Runtime.Remoting.Contexts;
using BLL;
using DTO;
using System.Runtime.ConstrainedExecution;

namespace EmployeeManagementSystem
{
    public partial class AddEmployee : Form
    {
        private readonly EmployeeService _employeeService;

        public AddEmployee()
        {
            InitializeComponent();
            _employeeService = new EmployeeService();
            DisplayEmployeeData();
            DisableButton();
        }

        public void DisableButton()
        {
            btnAddEmployee.Enabled = false;
            btnAddEmployee.BackColor = Color.White;
        }

        public void DisplayEmployeeData()
        {
            List<Employee> employees = _employeeService.GetEmployees();
            dataGridView1.DataSource = employees;
        }

        public void ShowError(string message)
        {
            MessageBox.Show(message, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ShowSuccess(string message)
        {
            MessageBox.Show(message, "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        public void ShowException(Exception ex)
        {
            MessageBox.Show("Error: " + ex, "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }

        public void ClearFields()
        {
            txtEmployeeId.Text = "";
            txtFullname.Text = "";
            cmbGender.SelectedIndex = -1;
            txtPhone.Text = "";
            cmbPosition.SelectedIndex = -1;
            cmbStatus.SelectedIndex = -1;
            pictureEmployee.Image = null;
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            if (txtEmployeeId.Text == "" || txtFullname.Text == "" || cmbGender.Text == "" || txtPhone.Text == "" || cmbPosition.Text == "" || pictureEmployee.Image == null)
            {
                ShowError("Please fill all blank fields");
            }
            else
            {
                try
                {
                    string imagePath = Path.Combine(@"C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\Directory\", txtEmployeeId.Text.Trim() + ".jpg");
                    string directoryPath = Path.GetDirectoryName(imagePath);

                    if (!Directory.Exists(directoryPath))
                    {
                        Directory.CreateDirectory(directoryPath);
                    }

                    File.Copy(pictureEmployee.ImageLocation, imagePath, true);

                    Employee employee = new Employee
                    {
                        EmployeeID = txtEmployeeId.Text.Trim(),
                        Name = txtFullname.Text.Trim(),
                        Gender = cmbGender.Text.Trim(),
                        Contact = txtPhone.Text.Trim(),
                        Position = cmbPosition.Text.Trim(),
                        Image = imagePath,
                        Salary = 0,
                        Status = cmbStatus.Text.Trim()
                    };

                    _employeeService.AddEmployee(employee);
                    DisplayEmployeeData();
                    ShowSuccess("Added successfully!");
                    ClearFields();
                }
                catch (Exception ex)
                {
                    ShowException(ex);
                }
            }
        }

        private void btnImport_Click(object sender, EventArgs e)
        {
            try
            {
                OpenFileDialog dialog = new OpenFileDialog();
                dialog.Filter = "Image Files (*.jpg; *.png)|*.jpg;*.png";
                string imagePath = "";
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    imagePath = dialog.FileName;
                    pictureEmployee.ImageLocation = imagePath;
                }
            }
            catch (Exception ex)
            {
                ShowException(ex);
            }
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            if (txtEmployeeId.Text == "" || txtFullname.Text == "" || cmbGender.Text == "" || txtPhone.Text == "" || cmbPosition.Text == "" || cmbStatus.Text == "" || pictureEmployee.Image == null)
            {
                ShowError("Please fill all blank fields");
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to UPDATE " +
                    "Employee ID: " + txtEmployeeId.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        string imagePath = Path.Combine(@"C:\Users\Administrator.DESKTOP-2HKJCIP\source\repos\EmployeeManagementSystem\EmployeeManagementSystem\Directory\", txtEmployeeId.Text.Trim() + ".jpg");

                        Employee employee = new Employee
                        {
                            EmployeeID = txtEmployeeId.Text.Trim(),
                            Name = txtFullname.Text.Trim(),
                            Gender = cmbGender.Text.Trim(),
                            Contact = txtPhone.Text.Trim(),
                            Position = cmbPosition.Text.Trim(),
                            Image = imagePath,
                            Status = cmbStatus.Text.Trim()
                        };

                        _employeeService.UpdateEmployee(employee);
                        DisplayEmployeeData();
                        ShowSuccess("Update successfully!");
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        ShowException(ex);
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (txtEmployeeId.Text == "" || txtFullname.Text == "" || cmbGender.Text == "" || txtPhone.Text == "" || cmbPosition.Text == "" || cmbStatus.Text == "" || pictureEmployee.Image == null)
            {
                ShowError("Please fill all blank fields");
            }
            else
            {
                DialogResult check = MessageBox.Show("Are you sure you want to DELETE " +
                    "Employee ID: " + txtEmployeeId.Text.Trim() + "?", "Confirmation Message", MessageBoxButtons.YesNo, MessageBoxIcon.Information);

                if (check == DialogResult.Yes)
                {
                    try
                    {
                        _employeeService.DeleteEmployee(txtEmployeeId.Text.Trim());
                        DisplayEmployeeData();
                        ShowSuccess("Delete successfully!");
                        ClearFields();
                    }
                    catch (Exception ex)
                    {
                        ShowException(ex);
                    }
                }
                else
                {
                    MessageBox.Show("Cancelled.", "Information Message", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }
            }
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            ClearFields();
        }

        private void dataGridView1_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex != -1)
            {
                DataGridViewRow row = dataGridView1.Rows[e.RowIndex];
                txtEmployeeId.Text = row.Cells[1].Value.ToString();
                txtFullname.Text = row.Cells[2].Value.ToString();
                cmbGender.Text = row.Cells[3].Value.ToString();
                txtPhone.Text = row.Cells[4].Value.ToString();
                cmbPosition.Text = row.Cells[5].Value.ToString();

                string imagePath = row.Cells[6].Value.ToString();

                if (imagePath != null)
                {
                    pictureEmployee.Image = Image.FromFile(imagePath);
                }
                else
                {
                    pictureEmployee.Image = null;
                }

                cmbStatus.Text = row.Cells[8].Value.ToString();
            }
        }

        private void label10_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void btnSalary_Click(object sender, EventArgs e)
        {
            Salary salaryForm = new Salary();
            salaryForm.Show();
            this.Hide();
        }
    }

}
