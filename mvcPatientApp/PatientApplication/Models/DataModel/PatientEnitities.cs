using System;
using System.Collections.Generic;

namespace PatientApplication.Models.DataModel
{
    public class PatientEnitities
    {
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }

        public ContactDetails contactDetails { get; set; }
        static PatientEnitities()
        {
            ContactDetails contactDetails = new ContactDetails() { Home = new List<string>(), Work = new List<string>(), Mobile = new List<string>() };
        }
    }
    public class ContactDetails
    {
        public List<string> Home { get; set; }
        public List<string> Mobile { get; set; }
        public List<string> Work { get; set; }

    }
}