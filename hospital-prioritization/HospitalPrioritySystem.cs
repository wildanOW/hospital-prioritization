using hospital_prioritization;
using System.Collections.Generic;
using System.Linq;

public class HospitalPrioritySystem
{
    private LinkedList<Patient> patients = new LinkedList<Patient>();
    private Dictionary<int, Patient> patientLookup = new Dictionary<int, Patient>();
    private int arrivalCounter = 0;
    private int patientIdCounter = 0;

    public void AddPatient(string name, int severity)
    {
        patientIdCounter++;
        arrivalCounter++;
        var newPatient = new Patient(name, severity, arrivalCounter, patientIdCounter);
        
        patientLookup[newPatient.PatientID] = newPatient;

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
        patientLookup.Remove(served.PatientID);
        return served;
    }
    public List<Patient> GetPatients()
    {
        return patients.ToList();
    }

    public Patient? GetPatientById(int patientId)
    {
        if (patientLookup.TryGetValue(patientId, out var patient))
        {
            return patient;
        }
        return null;
    }
}
