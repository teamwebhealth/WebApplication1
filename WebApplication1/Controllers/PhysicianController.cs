using DataLayer;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using static WebApplication1.Models.Appointment;

namespace WebApplication1.Controllers
{
    public class PhysicianController : Controller
    {
        public static string DoctorName = "";
        public static string day = "";
        public DateTime date;
        // GET: Physician
        public ActionResult Index()
        {
            Common obj = new Common();
            Appointment model = new Appointment();
            model.doctordropdown = obj.Doctorlist();
            return View(model);
        }
        [HttpGet]
        public ActionResult Treat(string ReferenceNo)
        {
            Common obj = new Common();
            Appointment ph = obj.GetAppointmentModelReferenceId(ReferenceNo);
            return View(ph);
        }

        [HttpPost]
        public ActionResult Treat(Appointment model)
        {
            OPDManagementEntities db = new OPDManagementEntities();
            OPDManagement obj = db.OPDManagements.Where(a => a.ReferenceNo == model.ReferenceNo).FirstOrDefault();
            Appointment ph = new Appointment();
            if (obj != null)
            {
                obj.Treated = "Yes";
                obj.DoctorFeedback = model.DoctorFeedback;
                db.SaveChanges();
                ViewBag.Message = "Record updated successfully.";
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult GridWithJquery(FormCollection collection)
        {
            ViewBag.Doctor = collection["Doctor"];
            ViewBag.Date = collection["AppointmentDate"];
            DoctorName = ViewBag.Doctor;
            IFormatProvider fp = new CultureInfo("es-ES");

            day = ViewBag.Date;


            return View();
        }


        public ActionResult Details(string ReferenceNo)
        {
            Common obj = new Common();
            Appointment ph = obj.GetAppointmentModelReferenceId(ReferenceNo);
            return View(ph);
        }



        public JsonResult GridMvcWithJquery()
        {
            date = Convert.ToDateTime(day);
            OPDManagementEntities db = new OPDManagementEntities();
            var result = (from c in db.OPDManagements
                          join d in db.DoctorMasters on c.DoctorId equals d.SerialNo
                          where d.DoctorName == DoctorName && c.AppointmentDate== date.Date
                          select new Appointment
                          {
                              ReferenceNo = c.ReferenceNo,
                              FullName = c.FullName,
                              PhoneNo = c.PhoneNo.ToString(),
                              Email = c.EmailId,
                              Doctorcode = c.DoctorCode,
                              TimeSlot = c.AppointmentTime,
                              PatientAge = c.Age,
                              Gender = c.Gender,
                              AllergyDetail = c.AllergyDetail,
                              CaseDetail = c.Casedetail,
                          }).ToList();

            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            jsonResult.Add("Html", RenderRazorViewToString("~/Views/Physician/mvcgridpartial.cshtml", result));
            jsonResult.Add("Status", "Success");

            return Json(jsonResult, JsonRequestBehavior.AllowGet);
        }

        protected string RenderRazorViewToString(string viewName, object model)
        {
            if (model != null)
            {
                ViewData.Model = model;
            }
            using (var sw = new StringWriter())
            {
                var viewResult = ViewEngines.Engines.FindPartialView(ControllerContext, viewName);
                var viewContext = new ViewContext(ControllerContext, viewResult.View, ViewData, TempData, sw);
                viewResult.View.Render(viewContext, sw);
                viewResult.ViewEngine.ReleaseView(ControllerContext, viewResult.View);
                return sw.GetStringBuilder().ToString();
            }
        }
      


    }


}