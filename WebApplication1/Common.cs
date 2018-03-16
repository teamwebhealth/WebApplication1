using DataLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using WebApplication1.Models;

namespace WebApplication1
{
    public class Common
    {
        public string GetTimeSlot(int id)
        {
            OPDManagementEntities context = new OPDManagementEntities();
            return context.AvaibilityMasters.Where(a => a.SerialNo == id).Select(a => a.AvaibilitySlot).FirstOrDefault();
        }

        public List<Avaibility> AvaibilityList(int doctorId)
        {


            OPDManagementEntities context = new OPDManagementEntities();
            List<Avaibility> result = new List<Avaibility>();
            List<AvaibilityMaster> obj = new List<AvaibilityMaster>();
            if (doctorId == 0)
                obj = context.AvaibilityMasters.ToList();
            else
                obj = context.AvaibilityMasters.Where(a => a.DoctorId == doctorId).ToList();
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

        public Appointment GetAppointmentModelReferenceId(string refernceId)
        {
            Appointment model = new Appointment();
            OPDManagement objOPDManagement = new OPDManagement();
            OPDManagementEntities context = new OPDManagementEntities();
            objOPDManagement = context.OPDManagements.Where(a => a.ReferenceNo == refernceId).FirstOrDefault();
            if (objOPDManagement != null)
            {
                model.Email = objOPDManagement.EmailId;
                model.ReferenceNo = objOPDManagement.ReferenceNo;
                model.FullName = objOPDManagement.FullName;
                model.doctorId = objOPDManagement.DoctorId ?? 0;
                model.Doctorcode = objOPDManagement.DoctorCode;
                model.PhoneNo = Convert.ToString(objOPDManagement.PhoneNo);
                model.Age = Convert.ToInt32(objOPDManagement.Age);
                model.AllergyDetail = objOPDManagement.AllergyDetail;
                model.CaseDetail = objOPDManagement.Casedetail;
                model.CaseImagePath = objOPDManagement.CaseImage;
                model.Gender = objOPDManagement.Gender;
                model.PeviousPrescriptionPath = objOPDManagement.PreviousPrescreption;
                model.Treated = objOPDManagement.Treated;
                model.DoctorFeedback = objOPDManagement.DoctorFeedback;
                if(objOPDManagement.DoctorId!=null && objOPDManagement.DoctorId>0)
                {
                    model.DoctorName = Doctorlist().Where(a => a.SerialNo == objOPDManagement.DoctorId).Select(a => a.DoctorName).FirstOrDefault();
                }
            }
            return model;
        }
        
    }
}