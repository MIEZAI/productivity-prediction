/*
 * 作者：姚强
 * 界面描述：非稳态参能EUP评价子界面，显示数据和图表
 * 时间：2020-1-3
 * 修改人：
 * 修改时间：
 *
 */


using DevExpress.XtraCharts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Access;
using DevExpress.XtraGrid.Views.Base;
using System.Data.SqlClient;

namespace CapacityForecasting
{
    public partial class Form_Sub_h : Form
    {

        private DataTable dt = null;

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

        public Form_Sub_h()
        {
            InitializeComponent();
        }

        private void chartControl1_Click(object sender, EventArgs e)
        {

        }

        private void Form2_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //设置行号列宽度
             this.gridView1.IndicatorWidth = 40;
            DataTable datatable = DataBusiness.sql_PY.Get();
            gridControl1.DataSource = datatable;
            // this.gridView1.Columns["Well_Num"].Visible = false;
            refresh();
            #region 绘图

            //新建线
            Series series1 = new Series("压力", ViewType.Point);
            Series series2 = new Series("产量", ViewType.Line);

            #region 设置series的样式
            //设置series的样式
            PointSeriesView view1 = (PointSeriesView)series1.View;
            view1.Color = Color.Red;//颜色
            view1.PointMarkerOptions.Size = 3;//大小           
            // view1.PointMarkerOptions.Kind = Kind.Plus;//样式

            LineSeriesView view2 = (LineSeriesView)series2.View;
            view2.Color = Color.Green;
            #endregion

            #region 描点
            //描点
            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                series1.Points.Add(new SeriesPoint(datatable.Rows[i][0], datatable.Rows[i][1]));
            }
            this.chartControl1.Series.Add(series1);

            for (int i = 0; i < datatable.Rows.Count; i++)
            {
                series2.Points.Add(new SeriesPoint(datatable.Rows[i][0], datatable.Rows[i][2]));
            }
            this.chartControl1.Series.Add(series2);
            #endregion

            XYDiagram diagram = chartControl1.Diagram as XYDiagram;

            //设置X滚动轴
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXZooming = true;
            ((XYDiagram)this.chartControl1.Diagram).EnableAxisXScrolling = true;

            //新建pane
            XYDiagramPane Pane1 = new XYDiagramPane("per");
            diagram.Panes.Add(Pane1);


            diagram.AxisY.Alignment = AxisAlignment.Far;

            //翻转pane,即纵坐标为深度
            //  diagram.Rotated = true;
            //将两个pane横着排列
            // diagram.PaneLayoutDirection = PaneLayoutDirection.Horizontal;

            //将seeies放在Pane1里           
            view2.Pane = Pane1;

            #region Pane1的坐标轴
            //设置Pane1的坐标轴，只设置Y轴          
            diagram.SecondaryAxesY.Add(new SecondaryAxisY("PaneY"));
            diagram.SecondaryAxesY[0].Alignment = AxisAlignment.Near;
            diagram.SecondaryAxesY[0].GridLines.Visible = true;

            diagram.SecondaryAxesY[0].Title.Visible = true;
            diagram.SecondaryAxesY[0].Title.Alignment = StringAlignment.Center;
            diagram.SecondaryAxesY[0].Title.Text = "产量";




            //这里只用Y轴，让X轴保持一样，才能同步变化
            view2.AxisY = diagram.SecondaryAxesY[0];
            #endregion

            //设置title
            //this.chartControl1.Titles.Add(new ChartTitle());
            //this.chartControl1.Titles[0].Text = "图";

            #region Pane1的坐标轴
            //设置defauPane的坐标轴
            diagram.AxisY.Alignment = AxisAlignment.Near;
            diagram.AxisY.Title.Visible = true;
            diagram.AxisY.Title.Alignment = StringAlignment.Center;
            diagram.AxisY.Title.Text = "压力";

            diagram.AxisX.Title.Visible = true;
            diagram.AxisX.Title.Alignment = StringAlignment.Center;
            diagram.AxisX.Title.Text = "时间（小时）";
            #endregion

            #endregion




        }

        private void Form_Sub_Shown(object sender, EventArgs e)
        {
           
        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        private void chartControl1_Click_1(object sender, EventArgs e)
        {

        }

        private void gridView1_CustomDrawRowIndicator(object sender, DevExpress.XtraGrid.Views.Grid.RowIndicatorCustomDrawEventArgs e)
        {
            
            if (e.Info.IsRowIndicator)

            {

                e.Info.DisplayText = (e.RowHandle + 1).ToString();

            }
        }

        private void gridControl1_Click_1(object sender, EventArgs e)
        {

        }
        
        /// <summary>
        /// 
        /// 修改数据
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
            int Hours = (int)dr[1];
            int result = DataBusiness.sql_PY.Delete(Hours);
            if (result > 0)
            {
                CommonTools.ShowMessage.ShowTips("删除成功！");
            }
            //刷新数据
            refresh();
        }
    }
}
