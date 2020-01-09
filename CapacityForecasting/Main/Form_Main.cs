
/*
 *主界面窗体
 */

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using WHC.Framework.Commons;
using CommonTools;
using CapacityForecasting;
using CapacityForecasting.Main;
using CapacityForecasting.CNSJ_evaluation;
using CapacityForecasting.EUR_evaluation;
using CapacityForecasting.Stead_evaluation;

namespace CapacityForecasting
{
    public partial class Form_Main : DevExpress.XtraBars.Ribbon.RibbonForm
    {
        public Form_Main()
        {
            InitializeComponent();
        }
        Form Form_temp;
        private void Form1_Load(object sender, EventArgs e)
        {
            this.WindowState = FormWindowState.Maximized;
            //加载子窗口，赋值给form
            Form_temp = ChildWinManagement.LoadMdiForm(this, typeof(Form_Sub_v));
        }

        private void barButtonItem7_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            
        }

        private void barButtonItem20_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_CDI form = new Form_CDI();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
            //先关闭子窗口再重新加载，刷新
            Form_temp.Close();
            Form_temp = ChildWinManagement.LoadMdiForm(this, typeof(Form_Sub_h));
        }
        
        private void barButtonItem5_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            OpenFileDialog openDialog = new OpenFileDialog();
            openDialog.Filter = "Txt Files (*.txt;)|*.txt;";
            openDialog.Multiselect = false;

            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                
                MessageBox.Show(openDialog.FileName);
            }
            else
            {
            }
        }

        private void barButtonItem6_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            //这是测试用的
            ChildWinManagement.LoadMdiForm(this, typeof(Form_Single));
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_NewWell Form = new Form_NewWell();
            Form.StartPosition = FormStartPosition.CenterScreen;
            Form.ShowDialog();
        }

        private void barButtonItem22_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Per form = new Form_Per();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem23_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_WP form = new Form_WP();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem10_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            this.Close();
        }

        private void barButtonItem25_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ParameterFit form = new Form_ParameterFit();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem36_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
          
            Form_FMBC form = new Form_FMBC();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem35_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // ChildWinManagement.LoadMdiForm(this, typeof(Form_DA));
            Form_DAC form = new Form_DAC();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem31_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductionWellTest form = new ProductionWellTest();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

            
        }

        private void barButtonItem4_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            LoggingDataEntry form1 = new LoggingDataEntry();
            form1.StartPosition = FormStartPosition.CenterScreen;
            form1.ShowDialog();
            //先关闭子窗口再重新加载，刷新
            Form_temp.Close();
            Form_temp = ChildWinManagement.LoadMdiForm(this, typeof(Form_Sub_v));
           

        }

        private void barButtonItem26_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_AutomaticFit form = new Form_AutomaticFit();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem34_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
           // ChildWinManagement.LoadMdiForm(this, typeof(Form_FMB));
            Form_DAC form = new Form_DAC();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem16_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem24_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem27_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductionForecast form = new ProductionForecast();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

        }

        private void barButtonItem32_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem33_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductionForecast2 form = new ProductionForecast2();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem37_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {

        }

        private void barButtonItem12_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_ShaleContent form = new Form_ShaleContent();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();

        }

        private void barButtonItem13_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Porosity form = new Form_Porosity();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem11_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_WaterSaturation form = new Form_WaterSaturation();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem14_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_YoungsModulus form = new Form_YoungsModulus();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem41_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_CrustalStress form = new Form_CrustalStress();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem40_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_PoissonRatio form = new Form_PoissonRatio();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem39_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Form_Permeability form = new Form_Permeability();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem28_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            Geologicalengineering form = new Geologicalengineering();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }

        private void barButtonItem30_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            ProductionForecast1 form = new ProductionForecast1();
            form.StartPosition = FormStartPosition.CenterScreen;
            form.ShowDialog();
        }
    }
}
