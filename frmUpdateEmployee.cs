using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Employee_Management_System
{
    public partial class frmUpdateEmployee : Form
    {
        public frmUpdateEmployee(string Id, string FirstName, string LastName, string Email, string Department, string Position, string Phone, string HireDate)
        {
            InitializeComponent();

            txtID.Text = Id;
            txtID.Enabled = false;

            txtEmail.Text = Email;
            txtEmail.Enabled = true;

            txtPhone.Text = Phone;
            txtPhone.Enabled = true;

            txtFirstName.Text = FirstName;
            txtLastName.Text = LastName;
            cmbDepartment.Text = Department;
            cmbPosition.Text = Position;
            dtpHireDate.Text = HireDate;
        }

        public string NewFirstName { get; set; }
        public string NewLastName { get; set; }
        public string NewDepartment { get; set; }
        public string NewPosition { get; set; }
        public string NewHireDate { get; set; }
        public string NewEmail { get; set; }
        public string NewPhone { get; set; }

        private bool IsInformationEmpty()
        {
            return string.IsNullOrEmpty(txtFirstName.Text) || string.IsNullOrEmpty(txtLastName.Text) ||
                string.IsNullOrEmpty(cmbDepartment.Text) || string.IsNullOrEmpty(cmbPosition.Text) ||
                string.IsNullOrEmpty(txtEmail.Text) || string.IsNullOrEmpty(txtPhone.Text);
        }

        private bool IsValidInputs()
        {
            if (!Regex.IsMatch(txtFirstName.Text, @"^[a-zA-Z\s]+$") || !Regex.IsMatch(txtLastName.Text, @"^[a-zA-Z\s]+$"))
            {
                MessageBox.Show("First Name and Last Name must be contain letter only.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            if (dtpHireDate.Value.Date > DateTime.Today || dtpHireDate.Value.Year < 1980)
            {
                MessageBox.Show("Please enter a valid email address.", "Validation Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                dtpHireDate.Focus();
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

            return true;
        }

        private void SaveOperation()
        {
            NewFirstName = txtFirstName.Text;
            NewLastName = txtLastName.Text;
            NewDepartment = cmbDepartment.Text;
            NewPosition = cmbPosition.Text;
            NewHireDate = dtpHireDate.Text;
            NewEmail = txtEmail.Text;

            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (IsInformationEmpty())
            {
                MessageBox.Show("Empty Text. Fill the feild.", "Error message", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            if (!IsValidInputs())
                return;

            SaveOperation();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}
