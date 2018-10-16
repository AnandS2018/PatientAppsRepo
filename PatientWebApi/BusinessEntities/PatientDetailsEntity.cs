
namespace BusinessEntities
{ 
    /// <summary>
    /// Business Entity class for PatientDetail Service
    /// </summary>
    public class PatientDetailsEntity
    {
        public int DetailId { get; set; }
        public string PatientData { get; set; }
        public System.DateTime Created { get; set; }
    }
}
