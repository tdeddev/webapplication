using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MVCProject.Models
{
    public class RequestDATA:EmployeeDATA
    {

        private List<RequestDATA> _lstReq = new List<RequestDATA>();

        private DateTime _createdate = DateTime.Now;

        public List<RequestDATA> LSTREQ
        {
            get { return _lstReq; }
            set { _lstReq = value; }
        }



        public string REQCODE { get; set; }
        public string ACTION { get; set; }
        public string USERID { get; set; }
        public string TITLE { get; set; }
        public string DESCRIPTION { get; set; }
        public string APPROVEID { get; set; }
        public DateTime CREATEDATE
        {
            get { return _createdate; }

            set { _createdate = value; }


        }
    }
}