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
        public static string userName, password;
        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Variable declare for hold user name and password
             

            userName = txtUserName.Text.Trim();
            password = txtPassword.Text.Trim();

            //Database connection string with exception handel.
            try
            {
                string connectionDb = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
                SqlConnection sqlcon = new SqlConnection(connectionDb);
                string query = "SELECT * FROM tblUser WHERE UserName = '" + userName + "'AND password = '" + password + "'";
                SqlDataAdapter sda = new SqlDataAdapter(query, sqlcon);
                DataTable dtbl = new DataTable();
                sda.Fill(dtbl);
                sqlcon.Close();

                if (txtUserName.Text == string.Empty && txtPassword.Text == string.Empty)
                {
                    lblMessage.Text = "Please enter User Name & Password";
                    lblMessage.ForeColor = System.Drawing.Color.Red;
                }
                else
                {
                    // Conditional Statement User Name and Password validation of Admin  and customer Login.

                    if (userName == "admin" && password == "123")
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
                            lblMessage.Text = "User Name & Password WRONG, Try AGAIN";
                            lblMessage.ForeColor = System.Drawing.Color.Red;
                            attemptCount++;
                        }
                    }
                    //Appropriately handle the situation when a reasonable number of failed login attempts occur.
                    if (attemptCount >= 4)
                    {
                        txtUserName.Enabled = false;
                        txtPassword.Enabled = false;
                        btnLogin.Enabled = false;
                        lblMessage.Text = "You’ve been temporarily locked! Try again Later";
                        lblMessage.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error message: COULD NOT CONNECT TO DATABASE: " + ex);
            }
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Hide login form and show customer registation form
            frmRegister regForm = new frmRegister();
            this.Hide();
            regForm.Show();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void frmLogin_Load(object sender, EventArgs e)
        {

        }
    }
}
