using System;
using System.Collections.Generic;
using System.ComponentModel;
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
        public string FullName { get; set; }
        //  public string LastName { get; set; }

        [DisplayName("ContactNo")]
        public String PhoneNo { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Choose Doctor")]
        public List<Doctor> doctordropdown { get; set; }

        [DisplayName("Avaibility Time")]
        public List<Avaibility> AvaibilityList { get; set; }
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
        [DisplayName("Appoinment Date")]
        public string AppointmentDate{ get; set; }
    }
}