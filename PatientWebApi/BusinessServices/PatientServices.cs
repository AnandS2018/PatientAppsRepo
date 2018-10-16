using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using AutoMapper;
using BusinessEntities;
using DataModel;
using DataModel.UnitOfWork;
using System;


namespace BusinessServices
{
    /// <summary>
    /// Offers services for Patient specific CRUD operations
    /// </summary>
    public class PatientServices:IPatientServices
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Public constructor.
        /// </summary>
        public PatientServices(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        /// <summary>
        /// Method to create Patient entry in db
        /// </summary>
        /// <param name="PatientEntity"></param>
        /// <returns></returns>
        public int CreatePatient(PatientEnitities patientEntity)
        {
            if (patientEntity != null)
            {

                using (var scope = new TransactionScope())
                {
                    var patients = new PatientDetail
                    {

                        PatientData = Utility.ObjectConverter.ObjectToXMLGeneric<PatientEnitities>(patientEntity),
                        Created = DateTime.Now
                    };
                    _unitOfWork.PatientRepository.Insert(patients);
                    _unitOfWork.Save();
                    scope.Complete();
                    return 1;
                }
            }
            return 0;
        }

        /// <summary>
        /// Fetches all the patients.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<PatientDetailsEntity> GetAllPatients()
        {
            var patients = _unitOfWork.PatientRepository.GetAll().ToList();
            if (patients.Any())
            {
                Mapper.CreateMap<PatientDetail, PatientDetailsEntity>();
                var patientsModel = Mapper.Map<List<PatientDetail>, List<PatientDetailsEntity>>(patients);
                return patientsModel;
            }
            return null;
        }
    }
}
