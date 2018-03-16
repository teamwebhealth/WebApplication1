using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{

    public class Appointment
    {

        public int SerialNo { get; set; }
        [DisplayName("Case ID")]
        public string ReferenceNo { get; set; }

        [DisplayName("Patient Name")]
        [Required(ErrorMessage = "This Field is required.")]
        public string FullName { get; set; }
        //  public string LastName { get; set; }

        [DisplayName("ContactNo")]
        [Required(ErrorMessage = "This Field is required.")]
        public String PhoneNo { get; set; }

        [DisplayName("Email")]
        [Required(ErrorMessage = "This Field is required.")]
        public string Email { get; set; }

        [DisplayName("Doctor Code")]
        public string Doctorcode { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public int doctorId { get; set; }

        [DisplayName("Choose Doctor")]
        public List<Doctor> doctordropdown { get; set; }

        [Required(ErrorMessage = "This Field is required.")]
        public string TimeSlot { get; set; }

        [DisplayName("Avaibility Time")]
        public List<Avaibility> AvaibilityList { get; set; }
      
        [DisplayName("Appoinment Date")]
        public string AppointmentDate{ get; set; }

        [DisplayName("Age")]
        public int Age { get; set; }

        [DisplayName("Age")]
        public string PatientAge { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Any Allergies?")]
        public string AllergyDetail { get; set; }

        [DisplayName("Describe Your Case")]
        public string CaseDetail { get; set; }

     

        [DisplayName("Previous prescription")]
        public HttpPostedFileBase PeviousPrescription { get; set; }

        public string PeviousPrescriptionPath { get; set; }

        [DisplayName("Case Related Images")]
        public HttpPostedFileBase CaseImage { get; set; }

        public string CaseImagePath { get; set; }
        [DisplayName("Doctor Feedback")]
        public string DoctorFeedback { get; set; }
        public string Treated { get; set; }
        public string DoctorName { get; set; }
    }
    public class Doctor
    {
        public int SerialNo { get; set; }
        public string DoctorCode { get; set; }
        public string DoctorName { get; set; }
    }
    public class Avaibility
    {
        public int SerialNo { get; set; }
        public string Avaibilityslot { get; set; }

    }
}