using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace HomeApplicaceRentalSystem
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

           
        private void btnRegister_Click(object sender, EventArgs e)
        {
            string UserName, Password, ConPassword;

            UserName = txtUserName.Text.Trim();
            Password = txtPassword.Text.Trim();
            ConPassword = txtConfirmPassword.Text.Trim();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        private void linkLabelBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin loginForm = new frmLogin();
            this.Hide();
            loginForm.Show();
        }
    }
}
