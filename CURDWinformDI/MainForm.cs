using CURDWinformDI.Services;
using Microsoft.Extensions.DependencyInjection;
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
    public partial class MainForm : Form
    {
        private readonly ICustomerService _customerService;
        public MainForm(ICustomerService customerService)
        {
            _customerService = customerService;
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
        }

        private void cURDKháchHàngToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Customer customerForm = new Customer(_customerService);
            customerForm.MdiParent = this;
            customerForm.Show();
        }
    }
}
