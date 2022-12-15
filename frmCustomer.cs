using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

namespace HomeApplicaceRentalSystem
{
    public partial class frmCustomer : Form
    {
        public frmCustomer()
        {
            InitializeComponent();
        }

        // Database connection string declaration.
        static string connectionDb = System.Configuration.ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;
        SqlConnection sqlcon = new SqlConnection(connectionDb);

        public int ItemId;
        public string CustomerId;
        
        private void frmCustomer_Load(object sender, EventArgs e)
        {
            GetItemRecord();
            GetShoppingCardData();
            SelectedItemClear();
        }



        private void btnSearch_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtSearch.Text))
            {
                SearchByItemType();
            }
            else if (!string.IsNullOrEmpty(txtEnergyConsumption.Text))
            {
                SearchByEnergyConsumption();
            }
            else if (!string.IsNullOrEmpty(txtCost.Text))
            {
                SearchByCost();
            }
            else
            {
                MessageBox.Show("Please Enter your SEARCH Value in the Box!!", "Search", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void GetItemRecord()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblItem", sqlcon);
            DataTable dt = new DataTable();
            sqlcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dgvItem.DataSource = dt;
            sqlcon.Close();
        }

        //Search By Item Type name Method.
        private void SearchByItemType()
        {
            string searchText = txtSearch.Text;
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblItem WHERE ItemTypeName LIKE '%"+searchText+"%' ", sqlcon);
            DataTable dt = new DataTable();
            sqlcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dgvItem.DataSource = dt;
            sqlcon.Close();
        }

        //Search By Cost name Method.
        private void SearchByCost()
        {
            string searchText = txtCost.Text;
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblItem WHERE MonthlyFee LIKE '%" + searchText + "%' ", sqlcon);
            DataTable dt = new DataTable();
            sqlcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dgvItem.DataSource = dt;
            sqlcon.Close();
        }


        //Search By Cost name Method.
        private void SearchByEnergyConsumption()
        {
            string searchText = txtEnergyConsumption.Text;
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblItem WHERE EnergyConsumption LIKE '%" + searchText + "%' ", sqlcon);
            DataTable dt = new DataTable();
            sqlcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dgvItem.DataSource = dt;
            sqlcon.Close();
        }
        private void label1_Click(object sender, EventArgs e)
        {

        }

        private void btnClear_Click(object sender, EventArgs e)
        {
            Clear();
        }

        private void Clear()
        {
            txtSearch.Text = string.Empty;
            txtEnergyConsumption.Text = string.Empty;
            txtCost.Text = string.Empty;
        }

        private void btnSelectionClear_Click(object sender, EventArgs e)
        {
            SelectedItemClear();
        }

        private void SelectedItemClear()
        {
            lblTypeName.Text = string.Empty;
            lblBrand.Text =string.Empty;
            lblColor.Text = string.Empty;
            lblDescription.Text= string.Empty;
            lblEnergy.Text= string.Empty;
            lblDimensition.Text= string.Empty;
            lblItemName.Text= string.Empty;
            lblItemId.Text= string.Empty;
            lblMonthlyRent.Text= string.Empty;
            lblTotalPrice.Text= string.Empty;
            lblModel.Text = string.Empty;

            txtQuantity.Text = string.Empty;
            txtMonths.Text = string.Empty;


        }

        private void dgvItem_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            lblItemId.Text = Convert.ToInt32(dgvItem.SelectedRows[0].Cells[0].Value).ToString();
            lblTypeName.Text = dgvItem.SelectedRows[0].Cells[1].Value.ToString();
            lblItemName.Text = dgvItem.SelectedRows[0].Cells[2].Value.ToString();
            lblDescription.Text = dgvItem.SelectedRows[0].Cells[3].Value.ToString();
            lblBrand.Text = dgvItem.SelectedRows[0].Cells[4].Value.ToString();
            lblModel.Text = dgvItem.SelectedRows[0].Cells[5].Value.ToString();
            lblDimensition.Text = dgvItem.SelectedRows[0].Cells[6].Value.ToString();
            lblColor.Text = dgvItem.SelectedRows[0].Cells[7].Value.ToString();
            lblEnergy.Text = dgvItem.SelectedRows[0].Cells[8].Value.ToString();
            lblMonthlyRent.Text = dgvItem.SelectedRows[0].Cells[9].Value.ToString();
        }

        //Total cost calculation method.
        private void CostCalculation()
        {
            decimal monthlyRent = Convert.ToDecimal(lblMonthlyRent.Text);
            decimal quantity = Convert.ToDecimal(txtQuantity.Text.Trim());
            decimal months = Convert.ToDecimal(txtMonths.Text.Trim());

            decimal totalCost = (monthlyRent * quantity) * months ;

            lblTotalPrice.Text = "£ " + totalCost.ToString();
        }

        private void txtMonths_TextChanged(object sender, EventArgs e)
        {
            CostCalculation();
        }

        private void GetShoppingCardData()
        {
            SqlCommand cmd = new SqlCommand("SELECT * FROM tblCart", sqlcon);
            DataTable dt = new DataTable();
            sqlcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dgvMyShoppingCart.DataSource = dt;
            sqlcon.Close();
        }

        private void btnAddToCart_Click(object sender, EventArgs e)
        {
            AddToCart();
        }

        private void AddToCart()
        {
            SqlCommand cmd = new SqlCommand("INSERT INTO tblCart VALUES (@ItemTypeName, " +
                   "@ItemName, @MonthlyRent, @Quantity, @TotalCost, @CustomerName, @ItemId)", sqlcon);

            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@ItemTypeName", lblTypeName.Text);
            cmd.Parameters.AddWithValue("@ItemName", lblItemName.Text);
            cmd.Parameters.AddWithValue("@MonthlyRent", Convert.ToDecimal(lblMonthlyRent.Text));
            cmd.Parameters.AddWithValue("@Quantity", Convert.ToInt32(txtQuantity.Text));
            cmd.Parameters.AddWithValue("@TotalCost", Convert.ToDecimal(lblTotalPrice.Text.Replace("£","").Trim()));
            cmd.Parameters.AddWithValue("@CustomerName", "TestCustomer");
            cmd.Parameters.AddWithValue("@ItemId", Convert.ToInt32(lblItemId.Text));

            sqlcon.Open();
            cmd.ExecuteNonQuery();
            sqlcon.Close();
            MessageBox.Show("Your Home Appliance Item Added Successfully!", "Select", MessageBoxButtons.OK, MessageBoxIcon.Information);


            GetShoppingCardData();
            //Clear();
        }

        private void btnDeleteItem_Click(object sender, EventArgs e)
        {
            DeleteItemFromCart();
        }

        //My Shopping Cart Item Delete Method.
        private void DeleteItemFromCart()
        {
            if (ItemId > 0)
            {
                SqlCommand cmd = new SqlCommand("DELETE FROM tblCart WHERE Id = @ItemId", sqlcon);
                cmd.CommandType = CommandType.Text;

                cmd.Parameters.AddWithValue("@ItemId", this.ItemId);
                sqlcon.Open();
                cmd.ExecuteNonQuery();
                sqlcon.Close();
                GetShoppingCardData();
                //Clear();
            }
            else
            {
                MessageBox.Show("Please select an home appliance item to delete !!", "Select?", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void dgvMyShoppingCart_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            ItemId = Convert.ToInt32(dgvMyShoppingCart.SelectedRows[0].Cells[0].Value);
        }

        private void TotalItemCartSum()
        {
            decimal sum = 0;

            for (int i = 0; i < dgvMyShoppingCart.Rows.Count; ++i)
            {
                sum += Convert.ToDecimal(dgvMyShoppingCart.Rows[i].Cells[5].Value);
            }

            lblGrandTotalCost.Text = "£ " + sum.ToString();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            TotalItemCartSum();
            MessageBox.Show("Your Order Has been Successfully Confirmed! ENJOY");
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.Hide();
            frmLogin loginForm = new frmLogin();
            loginForm.Show();
        }
    }
}
