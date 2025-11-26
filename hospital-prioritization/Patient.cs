namespace hospital_prioritization
{
    public class Patient
    {
        public int PatientID { get; set; }
        public string Name { get; set; }
        public int Severity { get; set; }
        public int ArrivalOrder { get; set; }

        public Patient(string name, int severity, int arrivalOrder, int patientId)
        {
            PatientID = patientId;
            Name = name;
            Severity = severity;
            ArrivalOrder = arrivalOrder;
        }
    }
}
