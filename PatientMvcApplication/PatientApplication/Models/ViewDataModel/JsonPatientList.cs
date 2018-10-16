using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientApplication.Models.ViewDataModel
{
    public class JsonPatientList
    {
        [JsonProperty]
        public string DetailId { get; set; }
        [JsonProperty]
        public string PatientData { get; set; }
        [JsonProperty]
        public DateTime Created { get; set; }
    }
}