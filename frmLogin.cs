using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace HomeApplicaceRentalSystem
{
    public partial class frmLogin : Form
    {
        public frmLogin()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            // Variable declare for hold user name and password
            string userName, password;

            userName = txtUserName.Text;
            password = txtPassword.Text;

            // Conditional Statement User Name and Password validation of Admin  and customer Login.
            if (userName == "admin" && password =="123")
            {
                this.Hide();
                frmAdmin adminForm = new frmAdmin();
                adminForm.Show();
            }
            else if (true)
            {
                var connDb = System.Configuration.ConfigurationManager.ConnectionStrings["HomeApplicaceRentalSystemDb"].ConnectionString;
                SqlConnection conn = new SqlConnection(connDb);

                SqlDataAdapter sda = new SqlDataAdapter("SELECT COUNT(*) FROM tblUser WHERE UserName =" + userName + " AND Password = " + password, conn);


            }
        }
    }
}
