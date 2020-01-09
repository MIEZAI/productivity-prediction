/*
 * 作者：姚强
 * 功能描述：读取txt文件
 * 日期：2020-1-3
 * 修改人：
 * 修改时间： 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class Read
    {

        /// <summary>
        /// 读取GeologicalEvaluation的数据并存入datatable
        /// </summary>
        /// <param name="path">源字符串</param>
        /// <returns>返回存入数据的datatable</returns>
        public static void Read_GE(string path)
        {


            //读取txt
            StreamReader rd = File.OpenText(path);
            //把第一行跳过                               
            string line;
            rd.ReadLine();

           

            DataTable datatable = new DataTable();

            datatable.Columns.Add("DEPTH", Type.GetType("System.Double"));
            datatable.Columns.Add("AC", Type.GetType("System.Double"));
            datatable.Columns.Add("GR", Type.GetType("System.Double"));
            datatable.Columns.Add("SP", Type.GetType("System.Double"));
            datatable.Columns.Add("DEN", Type.GetType("System.Double"));
            datatable.Columns.Add("POR", Type.GetType("System.Double"));
            datatable.Columns.Add("PER", Type.GetType("System.Double"));
            datatable.Columns.Add("SW", Type.GetType("System.Double"));


            while (!rd.EndOfStream)
            {
                line = rd.ReadLine();
                string[] data = line.Split('\t');//转换为int，存在数组
                double[] insert_row = new double[8];
                for (int i = 0; i < 8; i++)
                {
                    insert_row[i] = double.Parse(data[i]);
                }

                datatable.Rows.Add(insert_row[0], insert_row[1], insert_row[2], insert_row[3], insert_row[4], insert_row[5], insert_row[6], insert_row[7]);

            }

            //存入数据库
            DataBusiness.sql_GE.Insert_All(datatable);

        
        }

        /// <summary>
        /// 读取PressureYield的数据并存入datatable
        /// </summary>
        /// <param name="path">源字符串</param>
        /// <returns>返回存入数据的datatable</returns>
        public static void Read_PY(string path)
        {


            //读取txt
            StreamReader rd = File.OpenText(path);
            //把第一行跳过                               
            string line;
            rd.ReadLine();



            DataTable datatable = new DataTable();

            datatable.Columns.Add("Hours", Type.GetType("System.Int32"));
            datatable.Columns.Add("Yield", Type.GetType("System.Double"));
            datatable.Columns.Add("Gas", Type.GetType("System.Double"));
            datatable.Columns.Add("Water", Type.GetType("System.Double"));
           


            while (!rd.EndOfStream)
            {
                line = rd.ReadLine();
                string[] data = line.Split('\t');//转换为int，存在数组
                double[] insert_row = new double[4];
                for (int i = 0; i < 4; i++)
                {
                    insert_row[i] = double.Parse(data[i]);
                }

                datatable.Rows.Add(insert_row[0], insert_row[1], insert_row[2], insert_row[3]);

            }

            //存入数据库
            DataBusiness.sql_PY.Insert_All(datatable);


        }
    }
}
