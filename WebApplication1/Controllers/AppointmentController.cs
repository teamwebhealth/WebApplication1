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
            model.doctordropdown=Doctorlist();
            model.AvaibilityList = AvaibilityList();
            return View(model);
        }
        [HttpPost]
        public ActionResult SaveChanges(FormCollection formcollection)
       {
            Appointment model = new Appointment();

            model.doctordropdown = Doctorlist();
            model.AvaibilityList = AvaibilityList();
            return View(model);
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

        public List<Avaibility> AvaibilityList()
        {
            
          
                OPDManagementEntities context = new OPDManagementEntities();
            List<Avaibility> result = new List<Avaibility>();
            var obj = context.AvaibilityMasters.Where(u => !u.Doctor1.Contains("22-02-2018"));
            if (obj != null && obj.Count() > 0)
            {
                foreach (var data in obj)
                {
                    Avaibility model = new Avaibility();
                    model.SerialNo = data.SerialNo;
                    model.Avaibilityslot = data.AvaibilitySlot;
                  
                    result.Add(model);
                }
            }

            return result;
        }

        [DataType(DataType.Date)]
        public DateTime ReleaseDate { get; set; }


    }
}