using DataLayer;
using System;
using System.Collections.Generic;
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
        public static string DoctorName="";
        public static string day="" ;
        DateTime date = new DateTime();
        // GET: Physician
        public ActionResult Index()
        {
            Appointment model = new Appointment();
            model.doctordropdown = Doctorlist();
            return View(model);
        }

        public ActionResult Treat()
        {

            return View();
        }

        [HttpPost]
        public ActionResult GridWithJquery(FormCollection collection)
        {
            ViewBag.Doctor = collection["Doctor"];
            ViewBag.Date = collection["AppointmentDate"];
            DoctorName = ViewBag.Doctor;
            IFormatProvider fp = new CultureInfo("es-ES");
            date = DateTime.ParseExact(ViewBag.Date, "MM/dd/yyyy",fp);
            day = date.ToString("yyyy/MM/dd");
            return View();
        }

        
        public ActionResult Details(string ReferenceNo)
        {
            OPDManagementEntities db = new OPDManagementEntities();
            var obj = db.OPDManagements.Where(c => c.ReferenceNo.Equals(ReferenceNo));
            Physician ph = new Physician();
            if (obj != null && obj.Count() > 0)
            {
                foreach (var c in obj)
                {
                    ph.ReferenceNo = c.ReferenceNo;
                    ph.FullName = c.FullName;
                    ph.PhoneNo = c.PhoneNo.ToString();
                    ph.EmaiId = c.EmailId;
                    ph.DoctorCode = c.DoctorCode;
                    ph.AppointmentTime = c.AppointmentTime;
                    ph.AppointmentDate = c.AppointmentDate.Value.ToLongDateString();
                    ph.Age = c.Age;
                    ph.Gender = c.Gender;
                    ph.AllergyDetail = c.AllergyDetail;
                    ph.CaseDetail = c.Casedetail;
                    ph.PeviousPrescription = c.PreviousPrescreption;
                    ph.CaseImage = c.CaseImage;
                }
            }

            return View(ph);
        }



        public JsonResult GridMvcWithJquery()
        {
            OPDManagementEntities db = new OPDManagementEntities();
            var result = (from c in db.OPDManagements
                          join d in db.DoctorMasters on c.DoctorCode equals d.DoctorCode
                          where d.DoctorName==DoctorName && c.AppointmentDate.ToString() == day
                          select new Physician
                          {
                              ReferenceNo = c.ReferenceNo,
                              FullName = c.FullName,
                              PhoneNo = c.PhoneNo.ToString(),
                              EmaiId = c.EmailId,
                              DoctorCode = c.DoctorCode,
                              AppointmentTime =c.AppointmentTime,
                              Age=c.Age, 
                              Gender =c.Gender,
                              AllergyDetail =c.AllergyDetail,
                              CaseDetail =c.Casedetail,
                              PeviousPrescription=c.PreviousPrescreption,
                              CaseImage =c.CaseImage
                          }).ToList();

            Dictionary<string, object> jsonResult = new Dictionary<string, object>();
            jsonResult.Add("Html", RenderRazorViewToString("~/Views/Physician/mvcgridpartial.cshtml", result));
            jsonResult.Add("Status","Success");

            return Json(jsonResult,JsonRequestBehavior.AllowGet);
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
        public List<Doctor> Doctorlist()
        {
            OPDManagementEntities context = new OPDManagementEntities();
            List<Doctor> result = new List<Doctor>();
            var obj = context.DoctorMasters.Select(u => u).ToList();
            if (obj != null && obj.Count() > 0)
            {
                foreach (var data in obj)
                {
                    Doctor model = new Doctor();
                    model.SerialNo = data.SerialNo;
                    model.DoctorName = data.DoctorName;
                    model.DoctorCode = data.DoctorCode;
                    result.Add(model);
                }
            }

            return result;
        }


    }


}