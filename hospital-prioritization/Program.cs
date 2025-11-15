using hospital_prioritization;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

HospitalPrioritySystem hospital = new HospitalPrioritySystem();

app.MapGet("/", () => "Hospital Prioritization API is running!");

app.MapGet("/add", (string name, int severity) =>
{
    hospital.AddPatient(name, severity);
    return $"{name} added with severity {severity}.";
});

app.MapGet("/serve", () =>
{
    var patient = hospital.ServePatient();
    return (patient == null)
        ? "No patients in queue."
        : $"Serving patient: {patient.Name} (Severity {patient.Severity})";
});

app.MapGet("/list", () =>
{
    var list = hospital.GetPatients();

    if (list.Count == 0)
        return "No patients waiting.";

    string output = "Current Queue:\n";
    foreach (var p in list)
        output += $"{p.Name} — Severity {p.Severity} — Order {p.ArrivalOrder}\n";

    return output;
});

app.Run();