using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCProject.Controllers
{
    public class MenuController : Controller
    {
        // GET: Menu

       
        public ActionResult Index()
        {

            if (Session["USERID"] != null)
            {

                ViewBag.User = Session["USERID"].ToString();
            }
            else
            {
              

                return RedirectToAction("IndexLogin", "Login");
            }

            return PartialView();
        }
    }
}