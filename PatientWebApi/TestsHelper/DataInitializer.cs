using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DataModel; 
namespace TestsHelper
{
    public class DataInitializer
    {
        public static List<PatientDetail> GetAllPatients()
        {
            var patients = new List<PatientDetail>()
            {
                new PatientDetail()
                {
                    Created=DateTime.Now,
                    PatientData="testxml",
                    DetailId=1

                },
                new PatientDetail()
                {
                    Created=DateTime.Now,
                    PatientData="testxml2",
                    DetailId=2
                }
            };
            return patients;
        }
    }
}
