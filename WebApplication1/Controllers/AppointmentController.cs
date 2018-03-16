using DataLayer;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;
using static WebApplication1.Models.Appointment;

namespace WebApplication1.Controllers
{
    public class AppointmentController : Controller
    {
        // GET: Appointment
        public ActionResult Index()
        {
            Appointment model = new Appointment();
            Common obj = new Common();
            model.doctordropdown = obj.Doctorlist();
            return View(model);
        }
        [HttpPost]
        public ActionResult Index(Appointment model)
        {
            Common obj = new Common();
            model.doctordropdown = obj.Doctorlist();
            OPDManagement objOPDManagement = new OPDManagement();
            OPDManagementEntities context = new OPDManagementEntities();

            objOPDManagement.FullName = model.FullName;
            objOPDManagement.DoctorId = model.doctorId;
            objOPDManagement.EmailId = model.Email;
            objOPDManagement.PhoneNo = model.PhoneNo;
            objOPDManagement.AppointmentDate = Convert.ToDateTime(model.AppointmentDate);
            objOPDManagement.AvaibilityMasterId = Convert.ToInt16(model.TimeSlot);
            objOPDManagement.AppointmentTime = obj.GetTimeSlot(Convert.ToInt16(model.TimeSlot));
            objOPDManagement.DoctorCode = model.doctordropdown.Where(a => a.SerialNo == model.doctorId).Select(a => a.DoctorCode).FirstOrDefault();
            context.OPDManagements.Add(objOPDManagement);
            context.SaveChanges();
            return RedirectToAction("AppointmentBooked", "Appointment", new { refernceId = objOPDManagement.ReferenceNo });
        }
        [HttpGet]
        public ActionResult AppointmentBooked(string refernceId)
        {
            PatientLogin model = new PatientLogin();
            model.RefrenceNo = refernceId;
            return View(model);
        }
        [HttpPost]
        public ActionResult AppointmentBooked(PatientLogin model)
        {
            OPDManagementEntities context = new OPDManagementEntities();
            if (context.OPDManagements.Where(a => a.ReferenceNo == model.RefrenceNo && a.PhoneNo == model.MobileNumber).Count() > 0)
                return RedirectToAction("AddCaseDetail", "Appointment", new { refernceNo = model.RefrenceNo });
            return View(model);
        }
        [HttpGet]
        public ActionResult AddCaseDetail(string refernceNo)
        {
            Common obj = new Common();
            Appointment model = obj.GetAppointmentModelReferenceId(refernceNo);
            return View(model);
        }

        [HttpPost]
        public ActionResult AddCaseDetail(Appointment model)
        {
            OPDManagementEntities context = new OPDManagementEntities();
            OPDManagement objOPDManagement = new OPDManagement();
            objOPDManagement = context.OPDManagements.Where(a => a.ReferenceNo == model.ReferenceNo).FirstOrDefault();
            if (model.PeviousPrescription != null && model.PeviousPrescription.ContentLength > 0)
            {
                string pic = Guid.NewGuid() + System.IO.Path.GetExtension(model.PeviousPrescription.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/UploadFile"), pic);
                // file is uploaded
                model.PeviousPrescription.SaveAs(path);

                objOPDManagement.PreviousPrescreption = model.PeviousPrescriptionPath = "/UploadFile/" + pic;

            }
            if (model.CaseImage != null && model.CaseImage.ContentLength > 0)
            {
                string pic = Guid.NewGuid() + System.IO.Path.GetExtension(model.CaseImage.FileName);
                string path = System.IO.Path.Combine(
                                       Server.MapPath("~/UploadFile"), pic);
                // file is uploaded
                model.CaseImage.SaveAs(path);

                objOPDManagement.CaseImage = model.CaseImagePath = "/UploadFile/" + pic; ;

            }
            objOPDManagement.EmailId = model.Email;
            objOPDManagement.ReferenceNo = model.ReferenceNo;
            objOPDManagement.FullName = model.FullName;
            objOPDManagement.DoctorCode = model.Doctorcode;
            objOPDManagement.Age = Convert.ToString(model.Age);
            objOPDManagement.Gender = model.Gender;
            objOPDManagement.PhoneNo = model.PhoneNo;
            objOPDManagement.AllergyDetail = model.AllergyDetail;
            objOPDManagement.Casedetail = model.CaseDetail;
            context.SaveChanges();
            return RedirectToAction("AppointmentBooked", "Appointment", new { refernceId = string.Empty });
        }


        [HttpGet]
        public bool CheckLogin(string reference, string password)
        {
            OPDManagementEntities context = new OPDManagementEntities();
            return context.OPDManagements.Where(a => a.ReferenceNo.Trim().ToLower() == reference.Trim().ToLower() && a.PhoneNo == password).Count() > 0 ? true : false;   
        }

        [HttpGet]
        public ActionResult getAvailablityListAction(int doctorId, string Appointemntdate)
        {
            OPDManagementEntities context = new OPDManagementEntities();
            DateTime appointmentDate = Convert.ToDateTime(Appointemntdate);
            List<string> objExcludeTimeSlot = context.OPDManagements.Where(a => a.DoctorId == doctorId && a.AppointmentDate == appointmentDate).Select(a => a.AppointmentTime).ToList();
            var objAvaibilityMasters = context.AvaibilityMasters.Where(a => a.DoctorId == doctorId && !objExcludeTimeSlot.Contains(a.AvaibilitySlot)).Select(a => "<option value='" + a.SerialNo + "'>" + a.AvaibilitySlot + "</option>");

            return Content(String.Join("", objAvaibilityMasters));
        }
        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


    }
}