using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebApplication1.Models
{
    public class PatientLogin
    {
        [Required]
        public string RefrenceNo { get; set; }
        [Required]
        public string MobileNumber { get; set; }
    }
}