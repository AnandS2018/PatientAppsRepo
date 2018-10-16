using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientApplication.Settings
{
    public static class AppSettings
    {
      public static string key_Url_WebApi_BaseAddress = "ApiUrlBaseAddress";
      public static string key_Url_Reource_WebApi_getAllPatient = "ApiUrlGetPatientList";
      public static string key_Url_Reource_WebApi_createPatient = "ApiUrlCreatePatient";
    }
}