using DevExpress.Utils;
using DevExpress.XtraCharts;
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

namespace CapacityForecasting
{
    public partial class Form_Single : Form
    {

        /// <summary>
        /// 用于存储实时数据
        /// </summary>
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

        public Form_Single()
        {
            InitializeComponent();
        }

        private void Form_FMB_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;

            // Create a new chart.
           

            // Create two series.
            Series series1 = new Series("Series 1", ViewType.Bar);
            Series series2 = new Series("Series 2", ViewType.Line);

            // Add points to them, with their arguments different.
            series1.Points.Add(new SeriesPoint("Asia", 80));
            series1.Points.Add(new SeriesPoint("Europe", 74));
            series1.Points.Add(new SeriesPoint("Australia", 96));
            series1.Points.Add(new SeriesPoint("North America", 85));

            series2.Points.Add(new SeriesPoint(new DateTime(2018, 02, 28), 420));
            series2.Points.Add(new SeriesPoint(new DateTime(2018, 05, 31), 375));
            series2.Points.Add(new SeriesPoint(new DateTime(2018, 08, 31), 490));
            series2.Points.Add(new SeriesPoint(new DateTime(2018, 11, 30), 560));

            // Add both series to the chart.
            chartControl1.Series.AddRange(new Series[] { series1, series2 });

            // Cast the second series's view to the LineSeriesView type.
            LineSeriesView myView = (LineSeriesView)series2.View;

            // Hide the legend (optional).
            chartControl1.Legend.Visibility = DefaultBoolean.False;

            // Cast the chart's diagram to the XYDiagram type, 
            // to access its axes and panes.
            XYDiagram diagram = (XYDiagram)chartControl1.Diagram;

            // Add a new additional pane to the diagram.
            diagram.Panes.Add(new XYDiagramPane("My Pane"));

            // Note that the created pane has the zero index in the collection,
            // because the existing Default pane is a separate entity.
            myView.Pane = diagram.Panes[0];

            // Add titles to panes.
            //diagram.DefaultPane.Title.Text = "Sales by Region (Units)";
            //diagram.DefaultPane.Title.Visibility = DefaultBoolean.True;
            //diagram.Panes[0].Title.Text = "Revenue (Millions of USD)";
            //diagram.Panes[0].Title.Visibility = DefaultBoolean.True;

            // Customize the pane layout.
            //diagram.PaneDistance = 10;
            //diagram.PaneLayout.AutoLayoutMode = PaneAutoLayoutMode.Linear;
            //diagram.PaneLayout.Direction = PaneLayoutDirection.Horizontal;
            //diagram.DefaultPane.LayoutOptions.ColumnSpan = 3;
            //diagram.Panes[0].LayoutOptions.ColumnSpan = 2;

            // Add secondary axes to the diagram, and adjust their options.
            diagram.SecondaryAxesX.Add(new SecondaryAxisX("My Axis X"));
            diagram.SecondaryAxesY.Add(new SecondaryAxisY("My Axis Y"));
            diagram.SecondaryAxesX[0].Alignment = AxisAlignment.Near;
            diagram.SecondaryAxesY[0].Alignment = AxisAlignment.Near;
            diagram.SecondaryAxesY[0].GridLines.Visible = true;

            diagram.SecondaryAxesX[0].Title.Visible = true;
            diagram.SecondaryAxesX[0].Title.Text = "111";
            diagram.SecondaryAxesY[0].Title.Visible = true;
            diagram.SecondaryAxesY[0].Title.Text = "123";

            // Assign both the additional pane and, if required,
            // the secondary axes to the second series.             
            myView.AxisX = diagram.SecondaryAxesX[0];
            myView.AxisY = diagram.SecondaryAxesY[0];

            diagram.AxisY.Title.Visible = true;
            diagram.AxisY.Title.Alignment = StringAlignment.Center;
            diagram.AxisY.Title.Text = "产量";

            diagram.AxisX.Title.Visible = true;
            diagram.AxisX.Title.Alignment = StringAlignment.Center;
            diagram.AxisX.Title.Text = "时间（小时）";

        }

        private void gridControl1_Click(object sender, EventArgs e)
        {

        }

        /// <summary>
        /// 修改
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
            dt = DataBusiness.sql_PY.Get();
            gridControl1.DataSource = dt;
            gridView1.Columns["Well_Num"].Visible = false;

        }

        /// <summary>
        /// 添加
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
        /// 删除
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

        private void contextMenuStrip1_Opening(object sender, CancelEventArgs e)
        {

        }
    }
}
