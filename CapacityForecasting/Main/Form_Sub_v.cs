/*
 * 作者：姚强
 * 界面描述：地质评价子界面，显示数据和图表
 * 时间：2020-1-3
 * 修改人：
 * 修改时间：
 *
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
using DevExpress.XtraCharts;
using System.Data.SqlClient;
using DevExpress.XtraGrid.Columns;

namespace CapacityForecasting.Main
{
    public partial class Form_Sub_v : DevExpress.XtraEditors.XtraForm
    {
        private DataTable dt = null;

        /// <summary>
        /// 刷新数据
        /// </summary>
        private void refresh()
        {
            //刷新数据
            DataTable datatable = DataBusiness.sql_GE.Get();
            gridControl1.DataSource = datatable;
            gridView1.Columns["Well_Num"].Visible = false;
           // gridView1.Columns["DEPTH"].OptionsColumn.AllowEdit = false;//不可编辑
        }

        public Form_Sub_v()
        {
            InitializeComponent();
        }
        private void printing()
        {
            DataTable datatable = DataBusiness.sql_GE.Get();
            gridControl1.DataSource = datatable;
            gridView1.Columns["Well_Num"].Visible = false;
           // gridView1.Columns["DEPTH"].OptionsColumn.AllowEdit = false;//不可编辑

            ///绘图
            this.chartControl1.Series.Clear();
            //新建线
            Series series1 = new Series("por", ViewType.Line);
            Series series2 = new Series("per", ViewType.Line);

            //描点
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                series1.Points.Add(new SeriesPoint(datatable.Rows[i][1], datatable.Rows[i][6]));
            }
            this.chartControl1.Series.Add(series1);

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                series2.Points.Add(new SeriesPoint(datatable.Rows[i][1], datatable.Rows[i][7]));
            }
            this.chartControl1.Series.Add(series2);


            XYDiagram diagram = chartControl1.Diagram as XYDiagram;
            //设置X滚动轴
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXScrolling = true;

            //新建pane
            XYDiagramPane Pane1 = new XYDiagramPane("per");
            diagram.Panes.Add(Pane1);

            diagram.AxisY.Alignment = AxisAlignment.Far;

            //翻转pane,即纵坐标为深度
            diagram.Rotated = true;
            //将两个pane横着排列
            diagram.PaneLayoutDirection = PaneLayoutDirection.Horizontal;

            //将seeies放在Pane1里
            ((XYDiagramSeriesViewBase)series2.View).Pane = Pane1;

            //设置title
            //this.chartControl1.Titles.Add(new ChartTitle());
            //this.chartControl1.Titles[0].Text = "图";

            //diagram.AxisY.Title.Visible = true;
            //diagram.AxisY.Title.Alignment = StringAlignment.Center;
            //diagram.AxisY.Title.Text = "por";

            diagram.AxisX.Title.Visible = true;
            diagram.AxisX.Title.Alignment = StringAlignment.Center;
            diagram.AxisX.Title.Text = "深度";
        }
        private void Form_Sub_v_Load(object sender, EventArgs e)
        {
            //窗口最大化
            this.WindowState = FormWindowState.Maximized;

            //设置行号列宽度
            this.gridView1.IndicatorWidth = 55;

            DataTable datatable = DataBusiness.sql_GE.Get();
            gridControl1.DataSource = datatable;
            gridView1.Columns["Well_Num"].Visible = false;
           // gridView1.Columns["DEPTH"].OptionsColumn.AllowEdit = false;//不可编辑

            printing();
        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            if (e.Info.IsRowIndicator)

            {

                e.Info.DisplayText = (e.RowHandle + 1).ToString();

            }
        }

        /// <summary>
        /// 保存修改数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmUpdate_Click(object sender, EventArgs e)
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
            dt = DataBusiness.sql_GE.Get();
            gridControl1.DataSource = dt;
            gridView1.Columns["Well_Num"].Visible = false;
            printing();
        }

        /// <summary>
        /// 添加数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmInsert_Click(object sender, EventArgs e)
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

        /// <summary>
        /// 删除数据
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tsmDelete_Click(object sender, EventArgs e)
        {
            //获取当前行下标
            //int a = this.gridView1.FocusedRowHandle;

            //直接通过gridView获取当前行      
            DataRow dr = this.gridView1.GetDataRow(this.gridView1.FocusedRowHandle);
            //获取主键列的值
            double DEPTH = (double)dr[1];
            int result = DataBusiness.sql_GE.Delete(DEPTH);
            if (result > 0)
            {
                CommonTools.ShowMessage.ShowTips("删除成功！");
            }
            //刷新数据
            refresh();
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }
    }
}