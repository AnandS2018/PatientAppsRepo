using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Web.Mvc;
using PatientApplication.Models;
using PatientApplication.Settings;
using System.Configuration;
namespace PatientApplication.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    public class HomeController : Controller
    {
        static Models.DataModel.PatientEnitities patientEnitities = new  Models.DataModel.PatientEnitities() { contactDetails = new Models.DataModel.ContactDetails() { Home=new List<string>(),Work=new List<string>(),Mobile=new List<string>()} };
        static Boolean _hasContact = false;
        static List<Models.TelephoneDataModel> televm = new List<Models.TelephoneDataModel>();
        public ActionResult Index()
        {
            
            return View();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public ActionResult createPatient()
        {
            PatientDetail patientDetail = new PatientDetail() { TypeId = PatientDetail.TelephoneType.Home,Telephones=new List<Models.TelephoneDataModel>() { } };
            televm = new List<TelephoneDataModel>();
            return View(patientDetail);
        }
       
        

        /// <summary>
        /// 
        /// </summary>
        /// <param name="patientDetail"></param>
        /// <returns></returns>
      
       public ActionResult createPatient(PatientDetail patientDetail)
        {

            if (ModelState.IsValid)
            {
                try
                {
                    if (televm.Count == 0)
                    {
                        if (!string.IsNullOrWhiteSpace(patientDetail.TelephoneNumber))
                        {
                            Models.TelephoneDataModel telephoneDataModel = new Models.TelephoneDataModel();
                            telephoneDataModel.TelNumber = patientDetail.TelephoneNumber;
                            telephoneDataModel.TelType = patientDetail.TypeId.ToString();
                            televm.Add(telephoneDataModel);
                            patientDetail.Telephones = televm;
                            if (!_hasContact)
                            {
                                switch (patientDetail.TypeId.ToString())
                                {
                                    case "Home":
                                        patientEnitities.contactDetails.Home.Add(patientDetail.TelephoneNumber);
                                        break;
                                    case "Work":
                                        patientEnitities.contactDetails.Work.Add(patientDetail.TelephoneNumber);
                                        break;
                                    case "Mobile":
                                        patientEnitities.contactDetails.Mobile.Add(patientDetail.TelephoneNumber);
                                        break;
                                }
                            }
                        }
                    }
                    else
                    {

                    }
                    if (blnInsertToCreatepatient(patientDetail.ForeName, patientDetail.SurName, patientDetail.Gender, patientDetail.DOB))
                    {
                        if (blnCreatePatientViaApi(patientEnitities))
                        {
                            patientEnitities = new Models.DataModel.PatientEnitities() { contactDetails = new Models.DataModel.ContactDetails() { Home = new List<string>(), Work = new List<string>(), Mobile = new List<string>() } };
                            patientDetail = new PatientDetail() { TypeId = PatientDetail.TelephoneType.Home, Telephones = new List<Models.TelephoneDataModel> { } };
                            patientDetail.DOB = string.Empty;
                            patientDetail.ForeName = string.Empty;
                            patientDetail.SurName = string.Empty;
                            patientDetail.Gender = string.Empty;
                            patientDetail.TelephoneNumber = string.Empty;
                            televm = new List<Models.TelephoneDataModel>();
                        }
                    }

                }

                catch (Exception ex)
                {// do not add }
                 //return View(patientDetail,;

                }
                string uri = this.Request.UrlReferrer.AbsolutePath;
                return Redirect(uri);
            }
            else
                           
            return View("Newpatient",patientDetail);
        }

        
        public JsonResult AjaxPostCall(PatientDetail _patientDetail)
        {
            Models.TelephoneDataModel telephoneDataModel = new Models.TelephoneDataModel();
            PatientDetail patientDetail = new PatientDetail() { TypeId = PatientDetail.TelephoneType.Home, Telephones = new List<Models.TelephoneDataModel>() { } };
            try
            {
                patientDetail.ForeName = _patientDetail.ForeName;
                patientDetail.SurName = _patientDetail.SurName;
                patientDetail.DOB = _patientDetail.DOB;
                patientDetail.Gender = _patientDetail.Gender;

                if (!string.IsNullOrWhiteSpace(_patientDetail.TelephoneNumber))
                    {

                    telephoneDataModel.TelNumber = _patientDetail.TelephoneNumber;
                    telephoneDataModel.TelType = _patientDetail.TypeId.ToString();
                    televm.Add(telephoneDataModel);
                    patientDetail.Telephones = televm;
                    patientDetail.TelephoneNumber = string.Empty;
                    switch (_patientDetail.TypeId.ToString())
                    {
                        case "Home":
                            patientEnitities.contactDetails.Home.Add(_patientDetail.TelephoneNumber);
                            _hasContact = true;
                            break;
                        case "Work":
                            patientEnitities.contactDetails.Work.Add(_patientDetail.TelephoneNumber);
                            _hasContact = true;
                            break;
                        case "Mobile":
                            patientEnitities.contactDetails.Mobile.Add(_patientDetail.TelephoneNumber);
                            _hasContact = true;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {// do not add }
            }

            return Json(televm, JsonRequestBehavior.AllowGet);
        }
        bool blnInsertToCreatepatient(string _forName,string _surName,string _gender,string _DOB)
        {
            try
            {
                patientEnitities.DOB =_DOB;
                patientEnitities.ForeName = _forName;
                patientEnitities.SurName = _surName;
                patientEnitities.Gender = _gender;
                //patientEnitities.contactDetails.Home = _lsHome;
                //patientEnitities.contactDetails.Work = _lsWork;
                //patientEnitities.contactDetails.Home = _lsMobile;
                return true;

            }
            catch(Exception ex)
            {
                return false;
            }
        }
       bool  blnCreatePatientViaApi(Models.DataModel.PatientEnitities _patientEnitities)
        {
            try
            {
                HttpClient client = new HttpClient();
                client.BaseAddress = new Uri(ConfigurationManager.AppSettings[AppSettings.key_Url_WebApi_BaseAddress]);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                var response = client.PostAsJsonAsync(ConfigurationManager.AppSettings[AppSettings.key_Url_Reource_WebApi_createPatient], _patientEnitities).Result;
                
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }

}
