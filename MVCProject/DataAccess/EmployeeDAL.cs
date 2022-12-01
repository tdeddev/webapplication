using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using MVCProject.Models;

namespace MVCProject.DataAccess
{
    public class EmployeeDAL : Base
    {
        public bool CheckLogin(string userid , string password)
        {
            bool result = false;

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null; 

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = ActiveConnection();

            com.Connection = con;

            #endregion

            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select count(*) as C from tbluser where userid = '" + userid + "'  and  password = '" + password + "' ";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if(rd!=null && rd.HasRows)
            {
                while(rd.Read())
                {
                    int count =Convert.ToInt16(rd["C"].ToString());

                    if(count == 1)
                    {
                        result = true;


                    }
                 

                }

            }

            #endregion

            return result;

        }

        public EmployeeDATA GetUserInfo(string userid)
        {
            EmployeeDATA empdata = new EmployeeDATA();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion


            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select f.* , u.usertype from TBLUSERPROFILE f , TBLUSER u  where f.userid = u.userid  and u.userid = '" + userid + "'  ";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {
               
                while (rd.Read())
                {

                 
                    empdata.USERID = rd["USERID"].ToString(); //จาก rd["USERID"] ไป ยัง empdata properties USERID
                    empdata.FIRSTNAME = rd["FIRSTNAME"].ToString();
                    empdata.LASTNAME = rd["LASTNAME"].ToString();
                    empdata.DEPARTMENT = rd["DEPARTMENT"].ToString();
                    empdata.MOBILE = rd["MOBILE"].ToString();
                    empdata.POSITION = rd["POSITION"].ToString();
                    empdata.USERTYPE = rd["USERTYPE"].ToString();

                }

            }

            #endregion

            return empdata;

        }


        public List<EmployeeDATA> GetManagerInfo()
        {

            List<EmployeeDATA> listempdata = new List<EmployeeDATA>();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion


            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select * from TBLUSERPROFILE where Position = 'Manager'  ";

            #endregion

            #region Return Data

            rd = com.ExecuteReader();

            if (rd != null && rd.HasRows)
            {

                while (rd.Read())
                {

                    EmployeeDATA empdata = new EmployeeDATA();

                    empdata.USERID = rd["USERID"].ToString(); //จาก rd["USERID"] ไป ยัง empdata properties USERID
                    empdata.FIRSTNAME = rd["FIRSTNAME"].ToString();
                    empdata.LASTNAME = rd["LASTNAME"].ToString();
                    empdata.DEPARTMENT = rd["DEPARTMENT"].ToString();
                    empdata.MOBILE = rd["MOBILE"].ToString();
                    empdata.POSITION = rd["POSITION"].ToString();

                    listempdata.Add(empdata);

                }

            }

            #endregion

            return listempdata;

        }

     

        public EmployeeDATA GetUserTypeByUserId(string userid)
        {
            EmployeeDATA empdata = new EmployeeDATA();

            SqlCommand com = new SqlCommand();
            SqlDataReader rd = null;

            #region Connection Database

            SqlConnection con = new SqlConnection();

            con = this.ActiveConnection();
            com.Connection = con;

            #endregion


            #region Send Query

            com.CommandType = CommandType.Text;
            com.CommandText = "select usertype from TBLUSER where userid = @USERID";


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


                    empdata.USERTYPE = rd["USERTYPE"].ToString(); //จาก rd["USERID"] ไป ยัง empdata properties USERID
                  
                }

            }

            #endregion

            return empdata;

        }
    }
}