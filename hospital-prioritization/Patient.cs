namespace hospital_prioritization
{
    public class Patient
    {
        public string Name { get; set; }
        public int Severity { get; set; }
        public int ArrivalOrder { get; set; }

        public Patient(string name, int severity, int arrivalOrder)
        {
            Name = name;
            Severity = severity;
            ArrivalOrder = arrivalOrder;
        }
    }
}
