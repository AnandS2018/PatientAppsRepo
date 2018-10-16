using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PatientApplication.Models
{
    public class PatientDetail
    {
        [Required]
        [MinLength(3), MaxLength(50)]
        public string ForeName { get; set; }

        [Required]
        [MinLength(2), MaxLength(50)]
        //[StringLength(50, ErrorMessage = "The {0} must be at least {1} characters long.", MinimumLength = 3)]
        public string SurName { get; set; }
        
        [DataType(DataType.Date)]
        public string DOB { get; set; }

        [Required]
        public string Gender { get; set; }

        [DataType(DataType.PhoneNumber)]
        public string TelephoneNumber { get; set; }

        public TelephoneType TypeId { get; set; }

        public List<TelephoneDataModel> Telephones { get; set; }

        public enum TelephoneType : int
        {
            [Display(Name = "Home")]
            Home = 1,
            [Display(Name = "Mobile")]
            Mobile = 2,
            [Display(Name = "Work")]
            Work = 3
        }
        
    }
    /// <summary>
    /// 
    /// </summary>
    public class TelephoneDataModel
    {
        public String TelType { get; set; }
        
        public string TelNumber { get; set; }
    }
    /// <summary>
    /// 
    /// </summary>
    public class ContactDetails
    {
        public List<string> Number { get; set; }
    }
}