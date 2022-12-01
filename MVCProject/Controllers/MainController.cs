using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MVCProject.Models;

namespace MVCProject.Controllers
{
    public class MainController : Controller
    {
        // GET: Main
        public ActionResult Index()
        {
            ViewBag.Title = "HTML Helper";


            if (TempData["username"] != null && Session["username"]!=null)
            {
                string tempusername = TempData["username"].ToString();
                string sessionusername = Session["username"].ToString();

                //var data =

                //    new Dictionary<string, UserDATA>()

                //    { {"k001", new UserDATA { Username = "0001" , Name="Sarin" } }, { "k002", new UserDATA { Username = "0002", Name = "Teem" } }, { "k003", new UserDATA { Username = "0003", Name = "John" } } };


             var data = new Dictionary<string, UserDATA>()

              { {"00001" , new UserDATA{ Username="0001" , Name="Sarin" } } , {"0002" , new UserDATA{ Username="0002" , Name="Suwat"  } } };
                
                ViewData["datadic"] = data;

            }

            return View();
        }

        [HttpPost]
        public ActionResult Index(UserDATA data)
        {
            ViewBag.Title = "HTML Helper";

            return View();
        }
    }
}