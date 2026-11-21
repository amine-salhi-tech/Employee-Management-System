using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Security.Policy;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;


namespace Employee_Management_System
{
    public partial class frmMain : Form
    {
        public frmMain()
        {
            InitializeComponent();
        }

        private HashSet<Employee> GetEmployeesFormListView()
        {
            HashSet<Employee> employees = new HashSet<Employee>();

            foreach(ListViewItem item in lvEmployees.Items)
            {
                string id = item.Text;

                Employee emp = new Employee
                {
                    ID = id,
                    FirstName = item.SubItems[1].Text,
                    LastName = item.SubItems[2].Text,
                    Email = item.SubItems[3].Text,
                    Department = item.SubItems[4].Text,
                    Position = item.SubItems[5].Text,
                    Phone = item.SubItems[6].Text,
                    HireDate = item.SubItems[7].Text
                };

                employees.Add(emp);
            }

            return employees;
        }

        private Employee ConvertInformationToEmployeeObject()
        {
            Employee Employee = new Employee
            {
                ID = txtID.Text,
                FirstName = txtFirstName.Text,
                LastName = txtLastName.Text,
                Email = txtEmail.Text,
                Department = cmbDepartment.Text,
                Position = cmbPosition.Text,
                Phone = txtPhone.Text,
                HireDate = dtpHireDate.Text
            };

            return Employee;
        }

        private bool IsInformationEmpty()
        {
            return (string.IsNullOrEmpty(txtID.Text) || string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(cmbDepartment.Text) || string.IsNullOrEmpty(cmbPosition.Text) ||
                string.IsNullOrEmpty(txtPhone.Text) || string.IsNullOrEmpty(dtpHireDate.Text));
        }

        private bool IsEmailOrPhoneDuplicated(string Email, string Phone)
        {
            foreach (ListViewItem item in lvEmployees.Items)
            {
                string ExistingEmail = item.SubItems[3].Text;
                string ExistingPhone = item.SubItems[6].Text;

                if (ExistingEmail == Email)
                {
                    return true;
                }

                if (ExistingPhone == Phone)
                {
                    return true;
                }
            }

            return false;
        }

        private bool IsValidInputs()
        {
            if (!int.TryParse(txtID.Text, out int id) || id <= 0) 
            {
                MessageBox.Show("ID must be a valid positive number.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtID.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z\s]+$") || !Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z\s]+$")) 
            {
                MessageBox.Show("First Name and Last Name must be contain letter only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (!Regex.IsMatch(txtEmail.Text, @"^[^@\s]+@[^@\s]+\.[^@\s]+$")) 
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtEmail.Focus();
                return false;
            }

            if (!Regex.IsMatch(txtPhone.Text, @"^\d{8,15}$")) 
            {
                MessageBox.Show("Phone number must contain digit only (8 to 15 digit).", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                txtPhone.Focus();
                return false;
            }

            if (dtpHireDate.Value.Date > DateTime.Today || dtpHireDate.Value.Year < 1980) 
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpHireDate.Focus();
                return false;
            }

            return true;
        }

        private void AddNewEmployee()
        {
            ListViewItem item = new ListViewItem();

            item.Text = txtID.Text;
            item.SubItems.Add(txtFirstName.Text); 
            item.SubItems.Add(txtLastName.Text); 
            item.SubItems.Add(txtEmail.Text); 
            item.SubItems.Add(cmbDepartment.Text); 
            item.SubItems.Add(cmbPosition.Text);
            item.SubItems.Add(txtPhone.Text);
            item.SubItems.Add(dtpHireDate.Text);
       
            lvEmployees.Items.Add(item);

            lblTotalEmployees.Text = "Total Employees: " + lvEmployees.Items.Count;
        }

        private void AddEmployee()
        {
            if (IsInformationEmpty())
            {
                MessageBox.Show("Empty Text. Fill the feild.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidInputs())
                return;

            HashSet<Employee> Employees = GetEmployeesFormListView();

            Employee NewEmployee = ConvertInformationToEmployeeObject();

            if (Employees.Contains(NewEmployee))
            {
                MessageBox.Show("This employee already exist.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (IsEmailOrPhoneDuplicated(txtEmail.Text, txtPhone.Text))
            {
                MessageBox.Show("Email and phone is duplicated.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            AddNewEmployee();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            AddEmployee();
        }

        private void DeleteEmployee()
        {
            if (lvEmployees.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select employee from list.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            DialogResult result = MessageBox.Show("Are you sure you want to delete the selected employees ?", "Confirm Delete", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

            if (result == DialogResult.Yes)
            {
                while (lvEmployees.SelectedItems.Count > 0)
                {
                    lvEmployees.Items.Remove(lvEmployees.SelectedItems[0]);
                }

                lblTotalEmployees.Text = "Total Employees : " + lvEmployees.Items.Count;
            }
        }

        private void UpdateEmployee()
        {
            if (lvEmployees.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select employee from list.", "Error Message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            ListViewItem item = lvEmployees.SelectedItems[0];

            string ID = item.SubItems[0].Text;
            string FirstName = item.SubItems[1].Text;
            string LastName = item.SubItems[2].Text;
            string Email = item.SubItems[3].Text;
            string Department = item.SubItems[4].Text;
            string Position = item.SubItems[5].Text;
            string Phone = item.SubItems[6].Text;
            string HireDate = item.SubItems[7].Text;

            frmUpdateEmployee frm = new frmUpdateEmployee(ID, FirstName, LastName, Email, Department, Position, Phone, HireDate);

            if (frm.ShowDialog() == DialogResult.OK)
            {
                item.SubItems[1].Text = frm.NewFirstName;
                item.SubItems[2].Text = frm.NewLastName;
                item.SubItems[4].Text = frm.NewDepartment;
                item.SubItems[5].Text = frm.NewPosition;
                item.SubItems[7].Text = frm.NewHireDate;
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DeleteEmployee();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            lvEmployees.Items.Clear();

            lblTotalEmployees.Text = "Total Employees: " + lvEmployees.Items.Count;
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            UpdateEmployee();
        }
    }
}