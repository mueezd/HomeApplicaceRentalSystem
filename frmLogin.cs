using System;
using System.Data;
using System.Data.SqlClient;
using System.Windows.Forms;

namespace HomeApplicaceRentalSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        int attemptCount = 0;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Variable declare for hold user name and password
            string userName, password;

            userName = txtUserName.Text.Trim();
            password = txtPassword.Text.Trim();

            var connectionDb = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

            SqlConnection sqlcon = new SqlConnection(connectionDb);
            string query = "SELECT * FROM tblUser WHERE UserName = '"+ userName +"'AND password = '" + password + "'";
            SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
            DataTable dtbl = new DataTable();
            sda.Fill(dtbl);

            // Conditional Statement User Name and Password validation of Admin  and customer Login.

            if (txtUserName.Text == "admin" && txtPassword.Text == "123")
            {
                frmAdmin adminForm = new frmAdmin();
                this.Hide();
                adminForm.Show();
            }
            else
            {
                if (dtbl.Rows.Count == 1)
                {
                    frmCustomer customerForm = new frmCustomer();
                    this.Hide();
                    customerForm.Show();
                }
                else
                {
                    lblMessage.Text = "Check your user Name and password";
                    attemptCount++;
                }
            }

            if (attemptCount >= 4)
            {
                txtUserName.Enabled = false;
                txtPassword.Enabled = false;
                btnLogin.Enabled = false;
                lblMessage.Text = "You’ve been temporarily locked! Try again Later";
                lblMessage.ForeColor = System.Drawing.Color.Red;
            }

        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            //Customer Form Show 
            frmRegister regForm = new frmRegister();
            this.Hide();
            regForm.Show();
        }
    }
}
