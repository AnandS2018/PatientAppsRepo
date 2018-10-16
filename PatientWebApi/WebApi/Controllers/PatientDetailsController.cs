using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using BusinessEntities;
using BusinessServices;
using System.Web.Http;
using System;
using DataModel.UnitOfWork;
using WebApi.Helpers;

namespace WebApi.Controllers
{

    /// <summary>
    /// Conroller class for to handle Patient related transactions
    /// </summary>
    public class PatientDetailsController : ApiController
    {

        private readonly IPatientServices _patientServices;
        private readonly IUnitOfWork _unitOfWork;
        private readonly NLogger _urlHelper;
        #region Public Constructor

        public PatientDetailsController(IUnitOfWork unitOfWork) //(IUnitOfWork unitOfWork, IUrlHelper urlHelper)
        {
            _unitOfWork = unitOfWork;
           
        }
        /// <summary>
        /// Public constructor to initialize patient service instance
        /// </summary>
        public PatientDetailsController(IPatientServices patientServices)
        {
            _patientServices = patientServices;
        }


        //For Testability
    
        [HttpPost]
        public HttpResponseMessage SavePatient([FromBody] PatientEnitities patientForInsert)
        {
            int result = 0;
            HttpResponseMessage httpResponseMessage;
            if (patientForInsert == null)
            {
                httpResponseMessage = new HttpResponseMessage(HttpStatusCode.BadRequest);
                httpResponseMessage.Content = new ObjectContent(typeof(string), "Bad Request Received", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return httpResponseMessage;
            }
            try
            {
                result = _patientServices.CreatePatient(patientForInsert);
                if (result != 0)
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.Created);
                    httpResponseMessage.Content = new ObjectContent(typeof(string), "New Patient Record Created", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return httpResponseMessage;
                }
                else
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                    httpResponseMessage.Content = new ObjectContent(typeof(string), "An error occured while creating patient", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return httpResponseMessage;
                }
            }
            catch (Exception ex)
            {
                httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                httpResponseMessage.Content = new ObjectContent(typeof(string), "An error occured while creating patient", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return httpResponseMessage;
            }

        }

        /// <summary>
        /// Retrieve All patients
        /// </summary>
        /// <returns></returns>
        public HttpResponseMessage GetAllPatient()
        {
            HttpResponseMessage httpResponseMessage;
            try
            {
                var patients = _patientServices.GetAllPatients();
                if (patients != null)
                {
                    var patientEntities = patients as List<PatientDetailsEntity> ?? patients.ToList();
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.OK);
                    httpResponseMessage.Content = new ObjectContent(typeof(List<PatientDetailsEntity>), patientEntities, GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return httpResponseMessage;
                }
                else
                {
                    httpResponseMessage = new HttpResponseMessage(HttpStatusCode.NoContent);
                    httpResponseMessage.Content = new ObjectContent(typeof(string), "Patients data not found", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                    return httpResponseMessage;
                }

            }
            catch (Exception ex)
            {
                httpResponseMessage = new HttpResponseMessage(HttpStatusCode.InternalServerError);
                httpResponseMessage.Content = new ObjectContent(typeof(string), "An error occured while retriving patient inforamtion", GlobalConfiguration.Configuration.Formatters.JsonFormatter);
                return httpResponseMessage;
            }
        }
    }
}
#endregion