using System.Collections.Generic;
using BusinessEntities;

namespace BusinessServices
{
    /// <summary>
    /// patient Service Contract
    /// </summary>
    public interface IPatientServices
    {
        IEnumerable<PatientDetailsEntity> GetAllPatients();

        int CreatePatient(PatientEnitities patientEntity);

    }
}
