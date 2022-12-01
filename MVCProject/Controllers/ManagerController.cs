using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.DataAccess;
using MVCProject.Models;
using Newtonsoft.Json;
using System.Net;
using System.IO;
using MVCProject.AppService;
using System.Web.Script.Serialization;
using System.Text;

namespace MVCProject.Controllers
{
    public class ManagerController : Controller
    {
        // GET: Manager
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Form()
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

                    return RedirectToAction("IndexLogin", "Login");
                }
            

            }
         

            return PartialView(empdata);
        }

        [HttpPost]

        public ActionResult Form(string createdate,string userid , string HDCREATEDATE ,string btn,string ACTION)
        {

            EmployeeDATA empdata = new EmployeeDATA();
            RequestDATA reqdata = new RequestDATA();

            if (userid != null)
            {
            
                EmployeeDAL dal = new EmployeeDAL();

                empdata = dal.GetUserInfo(userid); //ดึง User Profile

                if (empdata != null && empdata.USERTYPE == "Manager")
                {


                    if (btn == "OK")
                    {
                        createdate = HDCREATEDATE;

                        RequestDAL req_dal = new RequestDAL();

                        List<RequestDATA> listdata = req_dal.GetRequest(createdate, userid);

                        ViewData["Result"] = listdata;


                    }
                    else if (createdate == "")
                    {
                        string action = ACTION;

                        string serviceUrl = "http://localhost:50615/RequestService.svc";
                 

                        AppService.ServiceRequestDATA input = new ServiceRequestDATA
                        {
                            ACTION = action

                        };

                     

                        JavaScriptSerializer jserialize = null;

                        jserialize = new JavaScriptSerializer();

          

                        string inputJson = jserialize.Serialize(input);


                        WebClient client = new WebClient();
                        client.Headers["Content-type"] = "application/json";
                        client.Encoding = Encoding.UTF8;

                       string strResult = client.UploadString(serviceUrl + "/GetRequestByCondition", inputJson);

                        ServiceRequestDATA listReq = jserialize.Deserialize<ServiceRequestDATA>(strResult);


                        List<RequestDATA> listresult = new List<RequestDATA>();

                        RequestDAL req_dal = new RequestDAL();


                        if (listReq.LSTREQ.Where(x=>x.APPROVEID.Trim()==userid).ToList().Count > 0)
                        {

                            foreach (ServiceRequestDATA service in listReq.LSTREQ.Where(x => x.APPROVEID.Trim() == userid))
                            {

                                listresult.Add(new RequestDATA {

                                    FIRSTNAME = req_dal.GetRequest(service.CREATEDATE.ToString("yyyy-MM-dd") , userid).FirstOrDefault().FIRSTNAME ,

                                    LASTNAME = req_dal.GetRequest(service.CREATEDATE.ToString("yyyy-MM-dd"), userid).FirstOrDefault().LASTNAME,

                                    ACTION = service.ACTION, DESCRIPTION = service.DESCRIPTION, REQCODE = service.REQCODE,  
                                    
                                    CREATEDATE = Convert.ToDateTime(service.CREATEDATE.ToString("yyyy-MM-dd")), TITLE = service.TITLE, APPROVEID = service.APPROVEID });
                            }

                            ViewData["Result"] = listresult;


                        }
                        else
                        {
                            ViewData["Result"] = null;

                        }

                     

                    }
                    else
                    {

                        RequestDAL req_dal = new RequestDAL();

                        List<RequestDATA> listdata = req_dal.GetRequest(createdate, userid);

                        ViewData["Result"] = listdata;

                    }


                }
                else
                {
                    return RedirectToAction("IndexLogin", "Login");

                }



            }



            return PartialView(empdata);
        }


        [HttpPost]

        public JsonResult Cancel(string reqcode)
        {
            RequestDAL dal = new RequestDAL();

            dal.RequestUpdate(reqcode, "Cancel");

            RequestDATA data = new RequestDATA();

            data.REQCODE = reqcode;
      
            return Json(data);

        }


        [HttpPost]

        public JsonResult Approve(string reqcode)
        {
            RequestDAL dal = new RequestDAL();

            dal.RequestUpdate(reqcode, "Approve");


            RequestDATA data = new RequestDATA();

            data.REQCODE = reqcode;

            return Json(data);


        }

    }
}

