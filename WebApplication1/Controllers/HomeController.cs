using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;

namespace WebApplication1.Controllers
{
   
    public class HomeController : Controller
    {
        
        public ActionResult Index()
        {
            SqlConnection Connection = new SqlConnection("Data Source=KRUNAL\\SQLEXPRESS;Initial Catalog=OPDManagement;Integrated Security=True");
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
        public ActionResult Appointment()
        {
            ViewBag.Message = "Your Appointment page.";

            return View();
        }
    }
}