using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class EmployeeDATA
    {
        private DateTime _createdate = DateTime.Now;


        public string USERID { get; set; }
        public string PASSWORD { get; set; }
        public string USERTYPE { get; set; }
        public string FIRSTNAME { get; set; }
        public string LASTNAME { get; set; }
        public string DEPARTMENT { get; set; }
        public string MOBILE { get; set; }
        public string GENDER { get; set; }
        public string POSITION { get; set; }
        public string JSON { get; set; }
        public DateTime CREATEDATE
        {
            get { return _createdate; }

            set { _createdate = value; }


        }
    }
}