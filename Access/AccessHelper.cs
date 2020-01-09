using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Access
{
    public class AccessHelper
    {
        private static string constr = "Provider=Microsoft.Jet.OLEDB.4.0;Data Source=../../Resource/CapacityForecasting.mdb";//"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=C:/Users/asus/Desktop/CapacityForecasting/CapacityForecasting/Resource/CapacityForecasting.mdb";
        private static OleDbConnection oc;
        public static OleDbDataAdapter mAdapter;
        /// <summary>
        /// 连接数据库
        /// </summary>
        private static void connToAcc()
        {
            oc = new OleDbConnection(constr);
            oc.Open();

        }

        /// <summary>
        /// 关闭数据库
        /// </summary>
        public static void ConnClose()
        {
            oc.Close();
        }

        /// <summary>
        /// 查询表1所有元素
        /// </summary>
        /// <returns></returns>
        public static DataTable SelectAll(String sSql)
        {
            DataTable ds = new DataTable();
            try
            {
                connToAcc();
                mAdapter = new OleDbDataAdapter(sSql, oc);
                OleDbCommandBuilder builder = new OleDbCommandBuilder(mAdapter);
                mAdapter.Fill(ds);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                CommonTools.ShowMessage.ShowError(e.ToString());

            }
            finally
            {
                ConnClose();
            }
            return ds;
        }
        /// <summary> 执行带参数的SQL语句，返回数据集
        /// </summary>
        /// <param name="sSql">SQL语句</param>
        /// <param name="Params">参数</param>
        /// <returns>返回查询结果</returns>
        public static DataSet QuerySql(string sSql, SqlParameter[] Params)
        {
            DataSet ds = new DataSet();
            OleDbCommand comm = new OleDbCommand();
            try
            {
                connToAcc();
                comm.Connection = oc;
                comm.CommandType = CommandType.Text;
                comm.CommandText = sSql;
                comm.Parameters.Clear();
                foreach (SqlParameter p in Params)
                {
                    comm.Parameters.Add(p);
                }
                mAdapter = new OleDbDataAdapter(comm);
                mAdapter.Fill(ds);
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                CommonTools.ShowMessage.ShowError(e.ToString());
            }
            finally
            {
                ConnClose();
            }
            return ds;
        }

        


        /// <summary>
        /// 删除操作
        /// </summary>
        /// <param name="sql">数据库语句</param>
        /// <returns>-1为错，其余为影响行数</returns>
        public static int Delete(String sql)
        {
            int iRet=0;
            
            try
            {
                connToAcc();
                OleDbCommand comm = new OleDbCommand(sql, oc);
                iRet = comm.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                CommonTools.ShowMessage.ShowError(e.ToString());
                return -1;

            }
            finally
            {
                ConnClose();
            }
            return iRet;
        }

        /// <summary>
        /// 插入操作
        /// </summary>
        /// <param name="sql">数据库语句</param>
        /// <returns>-1为错，其余为影响行数</returns>
        public static int Insert(String sql)
        {

            int iRet = 0;
           
            try
            {
                connToAcc();
                OleDbCommand comm = new OleDbCommand(sql, oc);
                iRet = comm.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                CommonTools.ShowMessage.ShowError(e.ToString());
                return -1;
            }
            finally
            {
                ConnClose();
            }
            return iRet;
        }

        /// <summary>
        /// 插入操作,插入整张表
        /// </summary>
        /// <param name="sql">数据库语句</param>
        /// <returns>-1为错，其余为影响行数</returns>
        public static int Insert_All(String[] sql)
        {

            int iRet = 0;

            try
            {
                connToAcc();
                foreach(string s in sql)
                {
                    OleDbCommand comm = new OleDbCommand(s, oc);
                    iRet += comm.ExecuteNonQuery();
                }
                
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                CommonTools.ShowMessage.ShowError(e.ToString());
                return -1;
            }
            finally
            {
                ConnClose();
            }
            return iRet;
        }

        /// <summary>
        /// 修改操作
        /// </summary>
        /// <param name="sql">数据库语句</param>
        /// <returns>-1为错，其余为影响行数</returns>
        public static int Update(String sql)
        {

             connToAcc();
            int iRet = 0;
             
            try
            {
             
                OleDbCommand comm = new OleDbCommand(sql,oc);
                iRet = comm.ExecuteNonQuery();
            }
            catch (System.Data.SqlClient.SqlException e)
            {
                CommonTools.ShowMessage.ShowError(e.ToString());
                return -1;
            }
            finally
            {
                ConnClose();
            }
            return iRet;
        }


    }
}
