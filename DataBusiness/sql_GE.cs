/*
 * 作者：姚强
 * 功能描述：对地质评价数据进行增删改查的sql语句
 * 日期：2020-1-3
 * 修改人：
 * 修改时间： 
 */

using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataBusiness
{
    public class sql_GE
    {

        /// <summary>
        /// 获取GeologicalEvaluation的所有数据
        /// </summary>      
        /// <returns>datatable</returns>
        public static DataTable Get()
        {          
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "select * from GeologicalEvaluation where Well_Num='" + wellid + "' order by DEPTH";
            DataTable datatable =Access.AccessHelper.SelectAll(sql);

            return datatable;
        }
        /// <summary>
        /// 查询某条数据是否存在
        /// </summary>      
        /// <returns>true/false</returns>
        public static bool select(double depth)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "select * from GeologicalEvaluation where Well_Num='" + wellid + "' and DEPTH=" + depth;
            DataTable datatable = Access.AccessHelper.SelectAll(sql);

            //找到返回true
            if(datatable.Rows.Count > 0)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        /// <summary>
        /// 往GeologicalEvaluatio中插入一条数据
        /// </summary>
        /// <param name="ge">GeologicalEvaluation实例</param>      
        /// <returns>受影响的行数</returns>
        public static int Insert(Entity.GeologicalEvaluation ge)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;


            int result = -1;
            //该数据不存在，就插入
            if (!select(ge.depth))
            {
                string sql = "insert into GeologicalEvaluation(Well_Num,DEPTH,AC,GR,SP,DEN,POR,PER,SW) values ('";
                sql += wellid + "',";
                sql += ge.depth + ",";
                sql += ge.AC + ",";
                sql += ge.GR + ",";
                sql += ge.SP + ",";
                sql += ge.DEN + ",";
                sql += ge.POR + ",";
                sql += ge.PER + ",";
                sql += ge.SW + ")";

               result = Access.AccessHelper.Insert(sql);
            }

           

            return result;
        }

        /// <summary>
        /// 往GeologicalEvaluatio中插入一张表
        /// </summary>
        /// <param name="ge">GeologicalEvaluation实例</param>      
        /// <returns>受影响的行数</returns>
        public static int Insert_All(DataTable datatable)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;
            string[] sql=new string[datatable.Rows.Count];

            //暂时线删除表，在插入
            Delete_All();

            int i = 0;
            foreach(DataRow dr in datatable.Rows)
            {
                
                   
                sql[i] = "insert into GeologicalEvaluation(Well_Num,DEPTH,AC,GR,SP,DEN,POR,PER,SW) values ('";
                sql[i] += wellid + "',";
                sql[i] += dr[0] + ",";
                sql[i] += dr[1] + ",";
                sql[i] += dr[2] + ",";
                sql[i] += dr[3] + ",";
                sql[i] += dr[4] + ",";
                sql[i] += dr[5] + ",";
                sql[i] += dr[6] + ",";
                sql[i] += dr[7] + ")";
                i++;
              
               
               
            }
           

            int result = Access.AccessHelper.Insert_All(sql);

            return result;
        }

        /// <summary>
        /// 修改GeologicalEvaluatio中的一条数据
        /// </summary>
        /// <param name="ge">GeologicalEvaluation实例</param>  
        /// <param name="depth">数据的主键</param>          
        /// <returns>受影响的行数</returns>
        public static int Update(Entity.GeologicalEvaluation ge,double depth)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

           

            string sql = "update GeologicalEvaluation set ";
            sql += "DEPTH=" + ge.depth;
            sql += ",AC=" + ge.AC;
            sql += ",GR=" + ge.GR;
            sql += ",SP=" + ge.SP;
            sql += ",DEN=" + ge.DEN;
            sql += ",POR=" + ge.POR;
            sql += ",PER=" + ge.PER;
            sql += ",SW=" + ge.SW;
            sql += " where DEPT=" + depth + ",Well_Num=" + wellid;


            int result = Access.AccessHelper.Update(sql);

            return result;
        }

        /// <summary>
        /// 删除GeologicalEvaluatio中的一条数据
        /// </summary>
        /// <param name="depth">数据的主键</param>      
        /// <returns>受影响的行数</returns>
        public static int Delete(double depth)
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "delete from GeologicalEvaluation where DEPTH=" + depth + " and Well_Num='" + wellid + "'";
            int result = Access.AccessHelper.Delete(sql);

            return result;
        }

        /// <summary>
        /// 删除GeologicalEvaluatio中一口井的数据
        /// </summary>
        /// <param name="depth">数据的主键</param>      
        /// <returns>受影响的行数</returns>
        public static int Delete_All()
        {
            //获取当前井号
            string wellid = Entity.Well.well_num;

            string sql = "delete from GeologicalEvaluation where Well_Num = '" + wellid + "'";
            int result = Access.AccessHelper.Delete(sql);

            return result;
        }

        /// <summary>
        /// 更新GeologicalEvaluatio中某一列的数据
        /// </summary>
        /// <param name="depth">数据的主键</param>      
        /// <returns>受影响的行数</returns>
        public static int Update_Row(string field, double change, char opt)
        {
            string wellid = Entity.Well.well_num;
            string sql = "update GeologicalEvaluation set " + field + "=" + field + "" + opt + "" + change + " where Well_Num = '" + wellid + "'";
            int result = Access.AccessHelper.Update(sql);
            return result;
        }
    }
}
