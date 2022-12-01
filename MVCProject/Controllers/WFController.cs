using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.DataAccess;
using MVCProject.Models;
using Newtonsoft.Json;



namespace MVCProject.Controllers
{
    public class WFController : Controller
    {
        // GET: WF
        public ActionResult Index()
        {
            EmployeeDATA empdata = new EmployeeDATA();
            RequestDATA reqdata = new RequestDATA();

            if (Session["USERID"] != null)
            {
                string userid = Session["USERID"].ToString();

                EmployeeDAL dal = new EmployeeDAL();

                empdata = dal.GetUserInfo(userid); //ดึง User Profile

                if (empdata != null && empdata.USERTYPE != "Manager")
                {

                    #region Get History

                    RequestDAL rdal = new RequestDAL();

                    List<RequestDATA> lstdata = rdal.GetLastHistory(userid);

                    reqdata.LSTREQ = lstdata;

                    string json = JsonConvert.SerializeObject(reqdata);

                    reqdata = JsonConvert.DeserializeObject<RequestDATA>(json);


                    if (reqdata.LSTREQ.Count > 0)
                    {

                        ViewData["REQDATA"] = reqdata.LSTREQ;

                    }

                    #endregion


                    #region Get ReqCode


                    ViewBag.REQCODE = rdal.GetReqCode();


                    #endregion

                }

                else
                {
                    return RedirectToAction("IndexLogin", "Login");

                }
            }

            return View(empdata);
        }

        [HttpPost] 

        public ActionResult Index(RequestDATA data)
        {

            RequestDAL dal = new RequestDAL();

            //บันทึก ข้อมูล Request

            dal.RequestInsert(data);

            //อัพเดทลง ตาราง tblcontroldoc

            int  docnum = Convert.ToInt16(data.REQCODE.Substring(6, data.REQCODE.Length - 6));
            string prefixdoc = "REQ";


            DocumentDAL docDal = new DocumentDAL();

            docDal.UpdateDocnum(docnum, prefixdoc);


            return Index();

        }

        [HttpPost]

        public ActionResult LoadApprover(EmployeeDATA data)
        {
            EmployeeDAL dal = new EmployeeDAL();

           List<EmployeeDATA> listempdata = dal.GetManagerInfo();


            return Json(listempdata);

        }

        [HttpPost]

        public JsonResult GetData(string userid)
        {
            RequestDAL rdal = new RequestDAL();

            List<RequestDATA> lstdata = rdal.GetRequest(userid);

            var result = lstdata.Select(x => new { x.REQCODE,x.FIRSTNAME, x.LASTNAME, x.TITLE, x.DESCRIPTION, x.APPROVEID  }).OrderBy(x => x.REQCODE).ToList();
                           
            return Json(result);

        }

        [HttpPost]
        public JsonResult DeleteData(string reqcode,string userid)
        {
            RequestDAL rdal = new RequestDAL();

            bool check = rdal.RequestDelete(reqcode);

            return GetData(userid);


        }

        [HttpGet]
        public JsonResult GetTitle()
        {

           
            List<TitleType> titles = new List<TitleType>

            { new TitleType { text = "สั่งซื้ออุปกรณ์", value = "T001" },

            new TitleType { text = "ลากิจ", value = "T002" } ,

             new TitleType { text = "ลาป่วย", value = "T003" },

             new TitleType { text = "ลาพักร้อน", value = "T004" },

             new TitleType { text = "เบิกค่ารักษาพยาบาล", value = "T005" },

             new TitleType { text = "อื่นๆ", value = "T006" }

            };


            return Json(titles, JsonRequestBehavior.AllowGet);


        }

    }

}