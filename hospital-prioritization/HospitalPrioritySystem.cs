using hospital_prioritization;
using System.Collections.Generic;
using System.Linq;

public class HospitalPrioritySystem
{
    private LinkedList<Patient> patients = new LinkedList<Patient>();
    private int arrivalCounter = 0;

    public void AddPatient(string name, int severity)
    {
        arrivalCounter++;
        var newPatient = new Patient(name, severity, arrivalCounter);

        if (patients.Count == 0)
        {
            patients.AddFirst(newPatient);
            return;
        }

        var current = patients.First;

        while (current != null)
        {
            if (newPatient.Severity > current.Value.Severity)
            {
                patients.AddBefore(current, newPatient);
                return;
            }

            if (newPatient.Severity == current.Value.Severity &&
                newPatient.ArrivalOrder < current.Value.ArrivalOrder)
            {
                patients.AddBefore(current, newPatient);
                return;
            }

            current = current.Next;
        }

        patients.AddLast(newPatient);
    }

    public Patient? ServePatient()
    {
        if (patients.Count == 0) return null;

        var served = patients.First.Value;
        patients.RemoveFirst();
        return served;
    }
    public List<Patient> GetPatients()
    {
        return patients.ToList();
    }
}
