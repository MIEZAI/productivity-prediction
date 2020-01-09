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

namespace CapacityForecasting.EUR_evaluation
{
    public partial class Form_FMBC : DevExpress.XtraEditors.XtraForm
    {
        public Form_FMBC()
        {
            InitializeComponent();
        }

        private void Form_FMBC1_Load(object sender, EventArgs e)
        {

        }

        private void btnInput_Click(object sender, EventArgs e)
        {
            //读取文件
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Txt Files (*.txt;)|*.txt;";
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog(null) == DialogResult.OK)
            {
                //读取txt，并保存在数据库中

            }
            else
            {
                CommonTools.ShowMessage.ShowError("路径有误！");
            }
        }

        private void btnExport_Click(object sender, EventArgs e)
        {

        }

        private void btnInsert_Click(object sender, EventArgs e)
        {

        }

        private void btnDelete_Click(object sender, EventArgs e)
        {

        }

        private void button5_Click(object sender, EventArgs e)
        {

        }

        private void simpleButton2_Click(object sender, EventArgs e)
        {

        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            string sql;
            double change = double.Parse(textBox1.Text);//变化数值
            string field = comboBox1.Text;                 //变化的数据列
            char opt;                                     //变化类型
            if (rbtnAdd.Checked)
            {
                opt = '+';
                sql = "update 表1 set " + field + "=" + field + "+" + change;
            }
            else if (rbtnReduce.Checked)
            {
                opt = '-';
                sql = "update 表1 set " + field + "=" + field + "-" + change;
            }
            else if (rbtnMultiplication.Checked)
            {
                opt = '*';
                sql = "update 表1 set " + field + "=" + field + "*" + change;
            }
            else if (rbtnDivide.Checked)
            {
                if (change == 0)
                    MessageBox.Show("0不能做除数");
                opt = '/';
                sql = "update 表1 set " + field + "=" + field + "/" + change;
            }
            else
            {
                MessageBox.Show("请勾选变化类型");
                return;
            }

            int result = Access.AccessHelper.Update(sql);
            if (result < 0)
            {
                MessageBox.Show("修改出错啦！");
            }
            else
            {
               
            }
        }

        private void btnCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCharge.PerformClick();
            }
        }

        private void simpleButton2_Click_1(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}