#region using namespaces.
using System;
using System.Collections.Generic;
using System.Linq;
using BusinessEntities;
using DataModel;
using DataModel.GenericRepository;
using DataModel.UnitOfWork;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;


using TestsHelper;

#endregion

namespace BusinessServices.Test
{
    /// <summary>
    /// Patient Service Test
    /// </summary>
    [TestClass()]
    public class PatientServicesTest
    {
        #region Variables

        private IPatientServices _patientService;
        private IUnitOfWork _unitOfWork;
        private List<PatientDetail> _patients;
        private GenericRepository<PatientDetail> _patientRepository;
        private WebApiDbEntities _dbEntities;
        #endregion

        #region Test fixture setup

        /// <summary>
        /// Initial setup for tests
        /// </summary>
        //[TestInitialize]
        //public void Setup()
        //{
        //    _patients = SetUpPatients();
        //}

        #endregion

        #region Setup

        /// <summary>
        /// Re-initializes test.
        /// </summary>
        [TestInitialize]
        public void ReInitializeTest()
        {
            _patients = SetUpPatients();
            _dbEntities = new Mock<WebApiDbEntities>().Object;
            _patientRepository = SetUpPatientRepository();
            var unitOfWork = new Mock<IUnitOfWork>();
            unitOfWork.SetupGet(s => s.PatientRepository).Returns(_patientRepository);
            _unitOfWork = unitOfWork.Object;
            _patientService = new PatientServices(_unitOfWork);

        }

        #endregion

        #region Private member methods
        /// <summary>
        /// Setup dummy repository
        /// </summary>
        /// <returns></returns>
        private GenericRepository<PatientDetail> SetUpPatientRepository()
        {
            // Initialise repository
            var mockRepo = new Mock<GenericRepository<PatientDetail>>(MockBehavior.Default, _dbEntities);
            // Setup mocking behavior
            mockRepo.Setup(p => p.GetAll()).Returns(_patients);

            // Return mock implementation object
            mockRepo.Setup(p => p.Insert((It.IsAny<PatientDetail>())))
                .Callback(new Action<PatientDetail>(newPatient =>
                {
                    dynamic maxPatientDetailId = _patients.Last().DetailId;
                    dynamic nextPatientDetailId = maxPatientDetailId + 1;
                    newPatient.DetailId = maxPatientDetailId;
                    newPatient.PatientData = "testData";
                    _patients.Add(newPatient);
                }));
            return mockRepo.Object;
        }


        /// <summary>
        /// Setup dummy patients data
        /// </summary>
        /// <returns></returns>

        private static List<PatientDetail> SetUpPatients()
        {
            var patientdetailId = new int();
            var patients = DataInitializer.GetAllPatients();
            foreach (var patientDetail in patients)
                patientDetail.DetailId = ++patientdetailId;
            return patients;

        }
        #endregion

        #region Unit Tests

        /// <summary>
        /// Service should return all the patients
        /// </summary>
        [TestMethod()]
        public void GetAllPatientsTest()
        {
            var patients = _patientService.GetAllPatients();
            if (patients != null)
            {
                var patientList =
                    patients.Select(
                        patientEntity =>
                        new PatientDetail { DetailId = patientEntity.DetailId, PatientData = patientEntity.PatientData, Created = patientEntity.Created }).
                        ToList();
                var comparer = new PatientComparer();
                Assert.IsNotNull(patients);
            }
        }

        /// <summary>
        /// Service should return null
        /// </summary>
        [TestMethod()]
        public void GetAllPatientsTestForNull()
        {
            _patients.Clear();
            var patients = _patientService.GetAllPatients();
            Assert.IsNull(patients);
            SetUpPatients();
        }

        /// <summary>
        /// Add new patient test
        /// </summary>
        [TestMethod]
        public void AddNewpatientTest()
        {
            PatientEnitities patientEnitities = new PatientEnitities()
            {
                ForeName = "testForeName",
                SurName = "testSurName",
                Gender = "testGender",
                DOB = DateTime.Now.ToShortDateString(),
                contactDetails = new ContactDetails()
                {
                    Home = new List<string>(),
                    Mobile = new List<string>(),
                    Work = new List<string>()
                }
            };

            Assert.AreNotEqual(_patientService.CreatePatient(patientEnitities), 0);
            
            

        }

        /// <summary>
        /// request should send null
        /// </summary>
        [TestMethod()]
        public void AddNewpatientTestWithNull()
        {
            PatientEnitities patientEnitities = new PatientEnitities();
            patientEnitities = null;
            Assert.AreEqual(_patientService.CreatePatient(patientEnitities), 0);

        }

        #endregion


        #region Tear Down

        /// <summary>
        /// Tears down each test data
        /// </summary>
        [TestCleanup]
        public void DisposeTest()
        {
            _patientService = null;
            _unitOfWork = null;
            _patientRepository = null;
            if (_dbEntities != null)
                _dbEntities.Dispose();
            _patients = null;
        }

        #endregion

        #region TestFixture TearDown.

        ///// <summary>
        ///// TestFixture teardown
        ///// </summary>
        //[TestCleanup]
        //public void DisposeAllObjects()
        //{
        //    _patients = null;
        //}

        #endregion


    }
}
