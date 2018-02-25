using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class Physician
    {
        public int SerialNo { get; set; }

        [DisplayName("Case Id")]
        public string ReferenceNo { get; set; }

        [DisplayName("Patient's Name")]
        public string FullName { get; set; }

        [DisplayName("Contact No")]
        public string PhoneNo { get; set; }

        [DisplayName("Email")]
        public string EmaiId { get; set; }

        [DisplayName("Physician code")]
        public string DoctorCode { get; set; }

        [DisplayName("Appoinment Date")]
        public string AppointmentDate { get; set; }

        [DisplayName("Appoinment Time")]
        public string AppointmentTime { get; set; }

        [DisplayName("Age")]
        public string Age { get; set; }

        [DisplayName("Gender")]
        public string Gender { get; set; }

        [DisplayName("Any Allergies?")]
        public string AllergyDetail { get; set; }

        [DisplayName("Describe Your Case")]
        public string CaseDetail { get; set; }

        [DisplayName("Previous prescription")]
        public byte[] PeviousPrescription { get; set; }

        [DisplayName("Case Related Images")]
        public byte[] CaseImage { get; set; }

        [DisplayName("IsTreated")]
        public int Treated { get; set; }
    }
}