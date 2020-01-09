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
using DevExpress.XtraGrid.Views.Grid;
using DevExpress.XtraGrid.Views.Grid.ViewInfo;
using DevExpress.XtraGrid.Columns;
using System.Data.SqlClient;

namespace CapacityForecasting
{
    public partial class Form_CDI : DevExpress.XtraEditors.XtraForm
    {
      
        DataTable dt=null;

        public Form_CDI()
        {
            InitializeComponent();
        }

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void refresh()
        {
            //刷新数据
            DataTable datatable = DataBusiness.sql_PY.Get();
            gridControl1.DataSource = datatable;
            gridView1.Columns["Well_Num"].Visible = false;
        }

        /// <summary>
        /// 文件读取
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnInput_Click(object sender, EventArgs e)
        {
            //读取文件
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Txt Files (*.txt;)|*.txt;";
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog(null) == DialogResult.OK)
            {
                //读取txt，并保存在数据库中
                DataBusiness.Read.Read_PY(openDialog.FileName);

                CommonTools.ShowMessage.ShowTips("导入完成！");
                //刷新数据
                refresh();
            }
            else
            {
                CommonTools.ShowMessage.ShowError("路径有误!");
            }
        }

        private void Form_CDI_Load(object sender, EventArgs e)
        {
            //设置行号列宽度
            this.gridView1.IndicatorWidth = 55;
        }

        private void btnOk_Click(object sender, EventArgs e)
        {
            dt = gridControl1.DataSource as DataTable;
            int id = gridView1.FocusedRowHandle;
            //更新数据
            try
            {
               
                int num = Access.AccessHelper.mAdapter.Update(dt);
            }
            catch (SqlException ex)
            {
                MessageBox.Show(ex.Message);
            }
            dt = DataBusiness.sql_PY.Get();
            gridControl1.DataSource = dt;
            gridView1.Columns["Well_Num"].Visible = false; 
        }

        private void Form_CDI_Shown(object sender, EventArgs e)
        {
            refresh();
        }

        private void btnInsert_Click(object sender, EventArgs e)
        {
        
            //生成空行
            int id = this.gridView1.FocusedRowHandle;//获取选中行下标

            dt = gridControl1.DataSource as DataTable;//获取数据
            DataRow dr = dt.NewRow();//添加新行

            dr[0] = Entity.Well.well_num;
            for (int i = 1; i < 4; i++)
            {
                dr[i] = DBNull.Value;//新行数据为空
            }
            dt.Rows.InsertAt(dr, id);
            gridControl1.DataSource = dt;
            gridView1.Columns["Well_Num"].Visible = false;

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void gridView1_CustomDrawRowIndicator(object sender, RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)

            {

                e.Info.DisplayText = (e.RowHandle + 1).ToString();

            }
        }

        private void btnCharge_Click(object sender, EventArgs e)
        {
            string sql;
            double change = 0;//变化数值
            string field = comboBox1.Text;                 //变化的数据列
            char opt;                                     //变化类型
            if (textBox1.Text=="")
            {
                CommonTools.ShowMessage.ShowWarning("请输入变换数据！");
            }
            else
            {
                change = double.Parse(textBox1.Text);
            }
            
            if (rbtnAdd.Checked)
            {
                opt = '+';              
            }
            else if (rbtnReduce.Checked)
            {
                opt = '-';
            }
            else if (rbtnMultiplication.Checked)
            {
                opt = '*';
            }
            else if (rbtnDivide.Checked)
            {
                if (change == 0)
                    CommonTools.ShowMessage.ShowWarning("0不能做除数");
                opt = '/';
            }
            else
            {
                CommonTools.ShowMessage.ShowWarning("请勾选变化类型！");
                return;
            }

            int result = DataBusiness.sql_PY.Update_Row(field, change, opt);
            if (result < 0)
            {
                CommonTools.ShowMessage.ShowError("修改出错啦！");
            }
            else
            {
                //刷新数据
                refresh();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            //获取当前行下标
            //int a = this.gridView1.FocusedRowHandle;

            //直接通过gridView获取当前行      
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            //获取主键列的值
            int Hours = (int)dr[1];
            int result = DataBusiness.sql_PY.Delete(Hours);
            if (result > 0)
            {
                CommonTools.ShowMessage.ShowTips("删除成功！");
            }
            //刷新数据
            refresh();
        }

        private void btnOk_KeyDown(object sender, KeyEventArgs e)
        {
            
        }

        private void btnCharge_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnCharge.PerformClick();
            }
        }
    }
}