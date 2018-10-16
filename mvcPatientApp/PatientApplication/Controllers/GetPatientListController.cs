using Newtonsoft.Json.Linq;
using PatientApplication.Models.DataModel;
using PatientApplication.Models.ViewDataModel;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Web.Mvc;
using PatientApplication.Utility;
using Utility;
using System.Configuration;

using PatientApplication.Settings;
using System.Net;

namespace PatientApplication.Controllers
{
    public class GetPatientListController : Controller
    {
        
        public ActionResult Index()
        {
            List<PatientEnitities> lstPatientEnitities = new List<PatientEnitities>();
            lstPatientEnitities = getPatientList();
            return View("GetPatientList", lstPatientEnitities);
        }

        public List<PatientEnitities> getPatientList()
        {
            List<PatientEnitities> lstPatientEnitities = new List<PatientEnitities>();
            try
            {
                WebConfigPropertyService webConfigPropertyService = new WebConfigPropertyService();
                using (var httpClient = new HttpClient())
                {
                    httpClient.DefaultRequestHeaders.Add("User-Agent", "Mozilla/5.0 (Windows NT 6.1; Win64; x64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/58.0.3029.110 Safari/537.36");
                    Uri uri = new Uri(string.Concat(ConfigurationManager.AppSettings[AppSettings.key_Url_WebApi_BaseAddress], ConfigurationManager.AppSettings[AppSettings.key_Url_Reource_WebApi_getAllPatient]));
                    var response = httpClient.GetStringAsync(uri).Result;
                    if (Response.StatusDescription == HttpStatusCode.OK.ToString())
                    {
                        
                        var releases = JArray.Parse(response);
                        List<JsonPatientList> myDeserializedObjList = (List<JsonPatientList>)Newtonsoft.Json.JsonConvert.DeserializeObject(response, typeof(List<JsonPatientList>));
                        foreach (var item in myDeserializedObjList)
                        {
                            PatientEnitities PatientEnitities = ObjectConverter.XMLToObject<PatientEnitities>(item.PatientData);
                            lstPatientEnitities.Add(PatientEnitities);
                        }
                    }
                }
            }
            catch (Exception ex)
            {

            }
            return lstPatientEnitities;
        }

    }
}