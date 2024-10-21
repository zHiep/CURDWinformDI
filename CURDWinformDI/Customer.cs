using CURDWinformDI.Services;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CURDWinformDI
{
    public partial class Customer : Form
    {
        private readonly ICustomerService _customerService;

        public Customer(ICustomerService customerService)
        {
            _customerService = customerService;
            InitializeComponent();
        }

        private void Customer_Load(object sender, EventArgs e)
        {
            LoadCustomersToGridView();
            btnCancel_Click(sender, e);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = btnAdd.Tag != null ? btnAdd.Tag.ToString() : string.Empty;
            _customerService.AddOrUpdateCustomer(id, txtTenKH.Text, txtEmail.Text, txtSDT.Text);
            btnCancel_Click(sender, e);
            LoadCustomersToGridView();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            txtTenKH.Text = txtSDT.Text = txtMaKH.Text = txtEmail.Text = txtSDT.Text = string.Empty;
            txtMaKH.Focus();
            btnAdd.Text = "Thêm Mới";
            btnUpdate.Enabled = true;
            btnDelete.Enabled = true;
            btnAdd.Tag = string.Empty;
            LoadCustomersToGridView();
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            DataRowView drvKhachHang = GetCurrentCustomer();
            if (drvKhachHang != null)
            {
                SwitchToEditMode(drvKhachHang);
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Bạn có muốn xóa khách hàng này không?", "Thông báo", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
            {
                return;
            }
            DataRowView drvKhachHang = GetCurrentCustomer();
            if (drvKhachHang != null)
            {
                _customerService.DeleteCustomer(drvKhachHang["Id"].ToString());
                LoadCustomersToGridView();
            }
        }

        private void LoadCustomersToGridView()
        {
            dgvKhachHang.DataSource = _customerService.GetAllCustomers();
        }

        private DataRowView GetCurrentCustomer()
        {
            if (dgvKhachHang.DataSource is DataTable dt)
            {
                DataView dvKhachHang = dt.DefaultView;
                return dvKhachHang[dgvKhachHang.CurrentRow.Index];
            }
            else if (dgvKhachHang.DataSource is DataView dv)
            {
                return dv[dgvKhachHang.CurrentRow.Index];
            }
            return null;
        }

        private void SwitchToEditMode(DataRowView drvKhachHang)
        {
            txtTenKH.Text = drvKhachHang["Name"].ToString();
            txtEmail.Text = drvKhachHang["Email"].ToString();
            txtSDT.Text = drvKhachHang["PhoneNumber"].ToString();
            txtMaKH.Text = drvKhachHang["Id"].ToString();
            btnAdd.Text = "Cập nhật";
            btnAdd.Enabled = true;
            btnAdd.Tag = drvKhachHang["Id"].ToString();
            btnUpdate.Enabled = btnDelete.Enabled = false;
        }

        private void txtSDT_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                e.SuppressKeyPress = true;
                btnAdd_Click(sender, e); 
            }
        }
    }
}
