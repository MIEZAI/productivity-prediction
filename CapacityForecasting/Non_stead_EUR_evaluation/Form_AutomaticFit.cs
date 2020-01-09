/*
 * 自动拟合窗口
 */ 

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using DevExpress.XtraEditors;

namespace CapacityForecasting
{
    public partial class Form_AutomaticFit : DevExpress.XtraEditors.XtraForm
    {
        public Form_AutomaticFit()
        {
            InitializeComponent();
        }

        private void Form_AutomaticFit_Load(object sender, EventArgs e)
        {

        }

        private void tabNavigationPage1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }
    }
}