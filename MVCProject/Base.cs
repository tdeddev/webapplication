using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Data;
using System.Configuration;

namespace MVCProject
{
    public class Base
    {
        public SqlConnection ActiveConnection()
        {

            SqlConnection con = new SqlConnection();


            string conStr = ConfigurationManager.ConnectionStrings["connection"].ConnectionString;

            con.ConnectionString = conStr; //set

            if(con!=null &&  con.State == ConnectionState.Closed)
            {
                con.Open();



            }


            return con; 
        }



    }
}