using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using MVCProject.Models;

namespace MVCProject.DataAccess
{
    public class RequestDAL : Base
    {
        public List<RequestDATA> GetLastHistory(string userid)
        {
            List<RequestDATA> lstdata = new List<RequestDATA>();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion

            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select u.USERID , u.FIRSTNAME , u.LASTNAME , u.DEPARTMENT , u.POSITION ," +
                " q.REQCODE , q.TITLE , q.description " +

                "  from TBLUSERPROFILE u, TBLREQUEST q where u.userid = q.userid and u.userid = '" + userid + "'" +
                "" +
                " and REQCODE = (select CONCAT('REQ000', max(Convert (INT , SUBSTRING(reqcode,4,len(reqcode)-3) )) ) as REQCODE  from TBLREQUEST where userid =  '" + userid + "'); ";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {


                while (rd.Read())
                {
                    RequestDATA reqdata = new RequestDATA();


                    reqdata.USERID = rd["USERID"].ToString();
                    reqdata.FIRSTNAME = rd["FIRSTNAME"].ToString();
                    reqdata.LASTNAME = rd["LASTNAME"].ToString();
                    reqdata.DEPARTMENT = rd["DEPARTMENT"].ToString();
                    reqdata.POSITION = rd["POSITION"].ToString();
                    reqdata.REQCODE = rd["REQCODE"].ToString();
                    reqdata.TITLE = rd["TITLE"].ToString();
                    reqdata.DESCRIPTION = rd["DESCRIPTION"].ToString();

                    lstdata.Add(reqdata);

                }

            }

            #endregion

            return lstdata;

        }

        public List<RequestDATA> GetRequest(string userid)
        {
            List<RequestDATA> lstdata = new List<RequestDATA>();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion

            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select  u.FIRSTNAME , u.LASTNAME , q.APPROVEID ," +
                " q.REQCODE , q.TITLE , q.description " +

                "  from TBLUSERPROFILE u, TBLREQUEST q where u.userid = q.userid and q.userid = '"+userid+"' ";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {


                while (rd.Read())
                {
                    RequestDATA reqdata = new RequestDATA();


                    
                    reqdata.FIRSTNAME = rd["FIRSTNAME"].ToString();
                    reqdata.LASTNAME = rd["LASTNAME"].ToString();
                    reqdata.APPROVEID = rd["APPROVEID"].ToString();
                    reqdata.REQCODE = rd["REQCODE"].ToString();
                    reqdata.TITLE = rd["TITLE"].ToString();
                    reqdata.DESCRIPTION = rd["DESCRIPTION"].ToString();

                    lstdata.Add(reqdata);

                }

            }

            #endregion

            return lstdata;

        }

        public List<RequestDATA> GetRequest(string createdate,string userid)
        {
            List<RequestDATA> lstdata = new List<RequestDATA>();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion

            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select  u.FIRSTNAME , u.LASTNAME , q.APPROVEID ," +
                " q.REQCODE , q.TITLE , q.description , q.CREATEDATE  , q.Action " +

                "  from TBLUSERPROFILE u, TBLREQUEST q where u.userid = q.userid and q.APPROVEID = '" + userid+"'  and q.CREATEDATE = CONVERT(datetime, '"+createdate+ "' , 20)   ";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {


                while (rd.Read())
                {
                    RequestDATA reqdata = new RequestDATA();



                    reqdata.FIRSTNAME = rd["FIRSTNAME"].ToString();
                    reqdata.LASTNAME = rd["LASTNAME"].ToString();
                    reqdata.APPROVEID = rd["APPROVEID"].ToString();
                    reqdata.REQCODE = rd["REQCODE"].ToString();
                    reqdata.TITLE = rd["TITLE"].ToString();
                    reqdata.DESCRIPTION = rd["DESCRIPTION"].ToString();
                    reqdata.CREATEDATE = Convert.ToDateTime(rd["CREATEDATE"]);
                    reqdata.ACTION = rd["ACTION"].ToString();

                    lstdata.Add(reqdata);

                }

            }

            #endregion

            return lstdata;

        }

        public string GetReqCode()
        {
            string reqcode = "";

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion


            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select * from tblcontroldoc";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {

                while (rd.Read())
                {

                    RequestDATA data = new RequestDATA();

                    data.REQCODE = rd["prefixdoc"].ToString() + "000" + Convert.ToString(Convert.ToInt16(rd["docnum"])+1);

                    //REQ00029

                    reqcode = data.REQCODE;


                }

            }

            #endregion

            return reqcode;
        }

        public bool RequestInsert(RequestDATA data)
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


                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "RequestInsert";

                SqlParameter param = null;

                param = new SqlParameter("@REQCODE", data.REQCODE);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;

                com.Parameters.Add(param);

                SqlParameter param2 = null;

                param2 = new SqlParameter("@USERID", data.USERID);
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;

                com.Parameters.Add(param2);

                SqlParameter param3 = null;

                param3 = new SqlParameter("@TITLE", data.TITLE);
                param3.Direction = ParameterDirection.Input;
                param3.DbType = DbType.String;

                com.Parameters.Add(param3);


                SqlParameter param4 = null;

                param4 = new SqlParameter("@DESCRIPTION", data.DESCRIPTION);
                param4.Direction = ParameterDirection.Input;
                param4.DbType = DbType.String;

                com.Parameters.Add(param4);



                SqlParameter param5 = new SqlParameter();

                param5 = new SqlParameter("@APPROVEID", data.APPROVEID);
                param5.Direction = ParameterDirection.Input;
                param5.DbType = DbType.String;

                com.Parameters.Add(param5);



                SqlParameter param6 = new SqlParameter();

                param6 = new SqlParameter("@CREATEDATE", data.CREATEDATE.ToString("dd/MM/yyyy"));
                param6.Direction = ParameterDirection.Input;
                param6.DbType = DbType.String;

                com.Parameters.Add(param6);




                SqlParameter param7 = new SqlParameter();

                param7 = new SqlParameter("@GENDER ", data.GENDER);
                param7.Direction = ParameterDirection.Input;
                param7.DbType = DbType.String;

                com.Parameters.Add(param7);

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

        public bool RequestDelete(string reqcode)
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


                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "RequestDelete";

                SqlParameter param = null;

                param = new SqlParameter("@REQCODE", reqcode);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;

                com.Parameters.Add(param);          
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

        public bool RequestUpdate(string reqcode , string action)
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


                com.CommandType = CommandType.StoredProcedure;
                com.CommandText = "RequestUpdate";

                SqlParameter param = null;

                param = new SqlParameter("@REQCODE", reqcode);
                param.Direction = ParameterDirection.Input;
                param.DbType = DbType.String;

                com.Parameters.Add(param);

                SqlParameter param2 = null;

                param2 = new SqlParameter("@ACTION", action);
                param2.Direction = ParameterDirection.Input;
                param2.DbType = DbType.String;

                com.Parameters.Add(param2);

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

        public List<RequestDATA> GetRequestByManager(string userid)
        {
            List<RequestDATA> lstdata = new List<RequestDATA>();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion

            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select u.USERID , u.FIRSTNAME , u.LASTNAME , u.DEPARTMENT , u.POSITION ," +
                " q.REQCODE , q.TITLE , q.description " +

                "  from TBLUSERPROFILE u, TBLREQUEST q where u.userid = q.userid and u.userid = @USERID  ";


            SqlParameter param = null;

            param = new SqlParameter("@USERID", userid);
          
            param.DbType = DbType.String;
            param.Direction = ParameterDirection.Input;

            com.Parameters.Add(param);


            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {


                while (rd.Read())
                {
                    RequestDATA reqdata = new RequestDATA();


                    reqdata.USERID = rd["USERID"].ToString();
                    reqdata.FIRSTNAME = rd["FIRSTNAME"].ToString();
                    reqdata.LASTNAME = rd["LASTNAME"].ToString();
                    reqdata.DEPARTMENT = rd["DEPARTMENT"].ToString();
                    reqdata.POSITION = rd["POSITION"].ToString();
                    reqdata.REQCODE = rd["REQCODE"].ToString();
                    reqdata.TITLE = rd["TITLE"].ToString();
                    reqdata.DESCRIPTION = rd["DESCRIPTION"].ToString();

                    lstdata.Add(reqdata);

                }

            }

            #endregion

            return lstdata;


        }

       
    }
}