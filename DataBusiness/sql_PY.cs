/*
 * 作者：冯健
 * 功能描述：对压力产量数据进行增删改查的sql语句
 * 日期：2020-1-3
 * 修改人：姚强
 * 修改时间： 2020-1-6
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class sql_PY
    {
        /// <summary>
        /// 获取PressureYield的所有数据
        /// </summary>      
        /// <returns>datatable</returns>
        public static DataTable Get()
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "select * from PressureYield where Well_Num='" + wellid+"'";
            DataTable datatable = Access.AccessHelper.SelectAll(sql);

            return datatable;
        }

        /// <summary>
        /// 查询某条数据是否存在
        /// </summary>      
        /// <returns>true/false</returns>
        public static bool select(double hours)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "select * from PressureYield where Well_Num='" + wellid + "',Hours=" + hours;
            DataTable datatable = Access.AccessHelper.SelectAll(sql);

            //找到返回true
            if (datatable.Rows.Count != 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 往PressureYield中插入一条数据
        /// </summary>
        /// <param name="ge">PressureYield实例</param>      
        /// <returns>受影响的行数</returns>
        public static int Insert(Entity.PressureYield py)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "insert into PressureYield(Hours,Well_Num,Pressure,Gas,Water) values (";
            sql += py.hours + ",";
            sql += "'"+ wellid + "',";
            sql += py.yield + ",";
            sql += py.gas + ",";
            sql += py.water + ")";

            int result = Access.AccessHelper.Insert(sql);

            return result;
        }

        /// <summary>
        /// 往PressureYield中插入一张表
        /// </summary>
        /// <param name="ge">PressureYield实例</param>      
        /// <returns>受影响的行数</returns>
        public static int Insert_All(DataTable datatable)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;
            string[] sql = new string[datatable.Rows.Count];

            //暂时线删除表，在插入
            Delete_All();

            int i = 0;
            foreach (DataRow dr in datatable.Rows)
            {


                sql[i] = "insert into PressureYield(Well_Num,Hours,Pressure,Gas,Water) values ('";
                sql[i] += wellid + "',";
                sql[i] += dr[0] + ",";
                sql[i] += dr[1] + ",";
                sql[i] += dr[2] + ",";
                sql[i] += dr[3] + ")";
                i++;


            }


            int result = Access.AccessHelper.Insert_All(sql);

            return result;
        }


        /// <summary>
        /// 修改PressureYield中的一条数据
        /// </summary>
        /// <param name="ge">GeologicalEvaluation实例</param>  
        /// <param name="depth">数据的主键</param>          
        /// <returns>受影响的行数</returns>
        public static int Update(Entity.PressureYield py, double hours)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "update PressureYield set ";
            sql += "Hour=" + py.hours;          
            sql += ",Pressure=" + py.yield;
            sql += ",Gas=" + py.gas;
            sql += ",Water=" + py.water;
            sql += " where Hours=" + hours + ",Well_Num='" + wellid +"'";


            int result = Access.AccessHelper.Update(sql);

            return result;
        }


        /// <summary>
        /// 删除PressureYield中的一条数据
        /// </summary>
        /// <param name="depth">数据的主键</param>      
        /// <returns>受影响的行数</returns>
        public static int Delete(double hours)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "delete from PressureYield where Hours=" + hours + " and Well_Num='" + wellid +"'";

            int result = Access.AccessHelper.Delete(sql);

            return result;
        }


        /// <summary>
        /// 删除PressureYield中一口井的数据
        /// </summary>
        /// <param name="depth">数据的主键</param>      
        /// <returns>受影响的行数</returns>
        public static int Delete_All()
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "delete from PressureYield where Well_Num = '" + wellid + "'";
            int result = Access.AccessHelper.Delete(sql);

            return result;
        }



        /// <summary>
        /// 更新PressureYield中某一列的数据
        /// </summary>
        /// <param name="depth">数据的主键</param>      
        /// <returns>受影响的行数</returns>
        public static int Update_Row(string field, double change, char opt)
        {
            string wellid = Entity.Well.well_num;
            string sql = "update PressureYield set " + field + "=" + field + "" + opt + "" + change + " where Well_Num = '" + wellid + "'";
            int result = Access.AccessHelper.Update(sql);
            return result;
        }
    }
}
