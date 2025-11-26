using hospital_prioritization;
using System.Collections.Generic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader();
    });
});

var app = builder.Build();

app.UseCors("AllowAll");
app.UseDefaultFiles();
app.UseStaticFiles();

HospitalPrioritySystem hospital = new HospitalPrioritySystem();

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
    return Results.Json(list);
});

app.MapGet("/patient/{patientId}", (int patientId) =>
{
    var patient = hospital.GetPatientById(patientId);
    return (patient == null)
        ? Results.NotFound()
        : Results.Json(patient);
});

app.Run();