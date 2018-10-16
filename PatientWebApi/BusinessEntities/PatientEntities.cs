using System;
using System.Collections.Generic;

namespace BusinessEntities
{

    //[Validator(typeof(PatientValidator))]
    public class PatientEnitities
    {
        public string ForeName { get; set; }
        public string SurName { get; set; }
        public string DOB { get; set; }
        public string Gender { get; set; }
        public ContactDetails contactDetails { get; set; }

    }
    public class ContactDetails
    {
        public List<string> Home { get; set; }
        public List<string> Mobile { get; set; }
        public List<string> Work { get; set; }
    }


}
