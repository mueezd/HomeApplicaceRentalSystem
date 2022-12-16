using System;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Windows.Forms;

namespace HomeApplicaceRentalSystem
{
    public partial class frmAdmin : Form
    {

        public frmAdmin()
        {
            InitializeComponent();
            
        }
        // Item id variable declaration.

        public int ItemId;

        public string ItemTypeName, ItemName, Description, Brand, Model, Dimensions, Color, EnergyConsumption;
        public decimal MonthlyRent;

        // Database connection string declaration.
        static string connectionDb = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(connectionDb);
        private void frmAdmin_Load(object sender, EventArgs e)
        {
            GetItemRecord();   
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (ItemId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tblItem WHERE Id = @ItemId", sqlcon);

                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ItemId", this.ItemId);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                MessageBox.Show("Home Appliance Item Deleted Successfully !", "Deleted", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetItemRecord();
                Clear();
            }
            else
            {
                MessageBox.Show("Please select an home appliance item to delete !!", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void llLogout_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            // Logout and login again
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
          
            if (IsValid())
            {
                SqlCommand cmd = new SqlCommand("INSERT INTO tblItem VALUES (@ItemTypeName, " +
                    "@ItemName, @Description, @Brand, @Model, @Dimensions, @Color, @EnergyConsumption," +
                    "@MonthlyRent)", sqlcon);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ItemTypeName", txtItemTypeName.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Brand", txtBrand.Text.Trim());
                cmd.Parameters.AddWithValue("@Model", txtModel.Text.Trim());
                cmd.Parameters.AddWithValue("@Dimensions", txtDimension.Text.Trim());
                cmd.Parameters.AddWithValue("@Color", txtColor.Text.Trim());
                cmd.Parameters.AddWithValue("@EnergyConsumption", txtEnergyConsumption.Text.Trim());
                cmd.Parameters.AddWithValue("@MonthlyRent", Convert.ToDecimal(txtMonthlyRent.Text));

                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                lblMessage.Text = "Home Appliance Item Successfully Added!";
                lblMessage.ForeColor = System.Drawing.Color.Green;

                GetItemRecord();
                Clear();
            }
        }

        //TextBox field clear method.
        private void Clear()
        {
            ItemId= 0;
            txtItemTypeName.Text= string.Empty;
            txtItemName.Text= string.Empty;
            txtDescription.Text= string.Empty;
            txtBrand.Text= string.Empty;
            txtModel.Text= string.Empty;
            txtDimension.Text= string.Empty;
            txtColor.Text= string.Empty;
            txtEnergyConsumption.Text= string.Empty;
            txtMonthlyRent.Text= string.Empty;

            txtItemTypeName.Focus();
        }


        
        //Get item data method.
        private void GetItemRecord()
        {
            

            SqlCommand cmd = new SqlCommand("SELECT * FROM tblItem", sqlcon);
            DataTable dt = new DataTable();
            sqlcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            sqlcon.Close();

            dgvItem.DataSource= dt;
        }

        //Admin form field validation method.
        private bool IsValid()
        {
            if (txtItemTypeName.Text == string.Empty)
            {
                MessageBox.Show("Please enter item type name!","Failed",MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtItemName.Text == string.Empty)
            {
                MessageBox.Show("Please enter item name!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtDescription.Text == string.Empty)
            {
                MessageBox.Show("Please enter description  name!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtBrand.Text == string.Empty)
            {
                MessageBox.Show("Please enter Brand name!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtModel.Text == string.Empty)
            {
                MessageBox.Show("Please Enter model name!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtDimension.Text == string.Empty)
            {
                MessageBox.Show("Please enter dimension!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtColor.Text == string.Empty)
            {
                MessageBox.Show("Please enter color of item!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtEnergyConsumption.Text == string.Empty)
            {
                MessageBox.Show("Please enter Energy Consumption!", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }
            else if (txtMonthlyRent.Text == string.Empty)
            {
                MessageBox.Show("Please enter Monthly Rent Amount", "Failed", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return false;
            }

            return true;
        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            //Call item data to Data Grid View to text boxes
            ItemId = Convert.ToInt32(dgvItem.SelectedRows[0].Cells[0].Value);
            txtItemTypeName.Text = dgvItem.SelectedRows[0].Cells[1].Value.ToString();
            txtItemName.Text = dgvItem.SelectedRows[0].Cells[2].Value.ToString();
            txtDescription.Text = dgvItem.SelectedRows[0].Cells[3].Value.ToString();
            txtBrand.Text = dgvItem.SelectedRows[0].Cells[4].Value.ToString();
            txtModel.Text = dgvItem.SelectedRows[0].Cells[5].Value.ToString();
            txtDimension.Text = dgvItem.SelectedRows[0].Cells[6].Value.ToString();
            txtColor.Text = dgvItem.SelectedRows[0].Cells[7].Value.ToString();
            txtEnergyConsumption.Text = dgvItem.SelectedRows[0].Cells[8].Value.ToString();
            txtMonthlyRent.Text = dgvItem.SelectedRows[0].Cells[9].Value.ToString();


        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            // Update Item to Database tblItem.
            if (ItemId > 0)
            {
                SqlCommand cmd = new SqlCommand("UPDATE tblItem " +
                    "SET ItemTypeName = @ItemTypeName, " +
                    "ItemName = @ItemName, " +
                    "Description = @Description, " +
                    "Brand = @Brand, " +
                    "Model = @Model, " +
                    "Dimensions = @Dimensions, " +
                    "Color = @Color, " +
                    "EnergyConsumption = @EnergyConsumption, " +
                    "MonthlyFee = @MonthlyRent " +
                    "WHERE Id = @ItemId", sqlcon);

                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@ItemTypeName", txtItemTypeName.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemName", txtItemName.Text.Trim());
                cmd.Parameters.AddWithValue("@Description", txtDescription.Text.Trim());
                cmd.Parameters.AddWithValue("@Brand", txtBrand.Text.Trim());
                cmd.Parameters.AddWithValue("@Model", txtModel.Text.Trim());
                cmd.Parameters.AddWithValue("@Dimensions", txtDimension.Text.Trim());
                cmd.Parameters.AddWithValue("@Color", txtColor.Text.Trim());
                cmd.Parameters.AddWithValue("@EnergyConsumption", txtEnergyConsumption.Text.Trim());
                cmd.Parameters.AddWithValue("@MonthlyRent", txtMonthlyRent.Text.Trim());
                cmd.Parameters.AddWithValue("@ItemId", this.ItemId);

                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();

                MessageBox.Show("Home Appliance Item Updated Successfully !", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Information);
                GetItemRecord();
                Clear();
            }
            else
            {
                MessageBox.Show("Please select item from data gridview.", "Updated", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }
    }
}
