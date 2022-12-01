using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MVCProject.Models;

namespace MVCProject.DataAccess
{
    public class DocumentDAL : Base
    {
        public bool UpdateDocnum(int docnum , string prefixdoc)
        {

            int count = 0;

            bool result = false;

            SqlConnection con = null;
            SqlCommand com = new SqlCommand();
            SqlTransaction tran = null;



            try
            {


                #region ข้อ 1 การเชื่อมต่อฐานข้อมูล

                con = this.ActiveConnection();

                com.Connection = con;

                #endregion

                #region ข้อ 2 การยิง Query


                tran = con.BeginTransaction(IsolationLevel.ReadCommitted);


                com.CommandType = CommandType.Text;
                com.CommandText = "update tblcontroldoc set docnum = "+docnum+ " where prefixdoc = '"+prefixdoc+"' ";


                com.Transaction = tran;


                #endregion

                #region ข้อ 3 การรีเทินผลลัพ

                count = com.ExecuteNonQuery();

                if (count > 0)
                {
                    result = true;

                    tran.Commit();


                }
                else
                {
                    result = false;

                    tran.Rollback();

                }



                #endregion


            }
            catch (Exception ex)
            {

                string x = ex.Message;

                //write x to log



                //insert to log


                tran.Rollback();


            }
            finally
            {
                if (com != null)
                {
                    com = null;

                }

                if ((con != null) && (con.State == ConnectionState.Open))
                {
                    con.Close();
                    con = null;

                }


            }

            return result;


        }

    }
}