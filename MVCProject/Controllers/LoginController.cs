using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;
using MVCProject.DataAccess;
using Newtonsoft.Json;

namespace MVCProject.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult IndexLogin()
        {
           
            ViewBag.Title = "Login to WorkFlow";
            ViewBag.Head = "WelCome";


            EmployeeDATA data = new EmployeeDATA();

            data.USERID = "UserID";
            data.PASSWORD = "Password";


            return View(data);
        }

        [HttpPost]
        public ActionResult IndexLogin(EmployeeDATA data)
        {
            ViewBag.Title = "Login to WorkFlow";
            ViewBag.Head = "WelCome";

            EmployeeDAL dal = new EmployeeDAL();

            bool chk = dal.CheckLogin(data.USERID, data.PASSWORD);

         
            if (chk==true)
            {
                TempData["USERID"] = data.USERID;
                Session["USERID"] = data.USERID;


               EmployeeDATA empdata = dal.GetUserTypeByUserId(data.USERID);
            
                if (empdata.USERTYPE != "Manager")
                {
                   

                    return RedirectToAction("Index", "WF");
                }
                else
                {
                   

                    return RedirectToAction("Index", "Manager");

                }
                
            }
            else
            {


                data.USERID = "UserID";
                data.PASSWORD = "Password";

                return View(data);

            }



        }

    }
}