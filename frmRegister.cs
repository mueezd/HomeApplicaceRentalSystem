using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;

namespace HomeApplicaceRentalSystem
{
    public partial class frmRegister : Form
    {
        public frmRegister()
        {
            InitializeComponent();
        }

        // Database connection string declaration.
        static string connectionDb = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(connectionDb);


        private void btnRegister_Click(object sender, EventArgs e)
        {
            string UserName, Password, ConPassword, UserType;

            UserName = txtUserName.Text.Trim();
            Password = txtPassword.Text.Trim();
            ConPassword = txtConfirmPassword.Text.Trim();
            UserType = "Customer";



            if (txtPassword.Text == txtConfirmPassword.Text)
            {
                if (IsValid())
                {
                    SqlCommand cmd = new SqlCommand("INSERT INTO tblUser VALUES (@UserName, @Password, @UserType)", sqlcon);

                    cmd.CommandType = CommandType.Text;
                    cmd.Parameters.AddWithValue("@UserName", UserName);
                    cmd.Parameters.AddWithValue("@Password", Password);
                    cmd.Parameters.AddWithValue("@UserType", UserType);

                    sqlcon.Open();
                    cmd.ExecuteNonQuery();
                    sqlcon.Close();

                    MessageBox.Show("Customer account created successfully ", "successfully", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    Clear();
                }
            }
            else
            {
                MessageBox.Show("\"Passwords do NOT match\"!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }


        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void linkLabelBackToLogin_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            frmLogin loginForm = new frmLogin();
            this.Hide();
            loginForm.Show();
        }

        private void Clear()
        {
            txtUserName.Text = string.Empty;
            txtPassword.Text = string.Empty;
            txtConfirmPassword.Text = string.Empty;
        }

        //Password validation method. 
        private bool IsValid()
        {
            var hasUpperChar = new Regex(@"[A-Z]+");
            var hasLowerChar = new Regex(@"[a-z]+");

            if (txtUserName.Text == string.Empty && txtPassword.Text == string.Empty && txtConfirmPassword.Text == string.Empty)
            {
                MessageBox.Show("Please fill-up the field!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!Regex.IsMatch(txtUserName.Text.Trim(), @"^[a-zA-Z0-9]+$"))
            {
                MessageBox.Show("The user name only contain Latter And Number!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!(txtPassword.Text.Length >= 8) || !(txtPassword.Text.Length <= 16))
            {
                MessageBox.Show("The Password length must between Eight (8) and SIXTEEN (16) characters!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (!hasUpperChar.IsMatch(txtPassword.Text.Trim()) && !hasLowerChar.IsMatch(txtPassword.Text.Trim()))
            {
                MessageBox.Show("The PASSWORD contain at least ONE (1) lowercase and ONE (1) uppercase letter!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }
    }
}
