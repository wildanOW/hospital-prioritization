const API_URL = 'http://localhost:5199';

async function addPatient() {
    const name = document.getElementById('name').value;
    const severity = document.getElementById('severity').value;

    if (!name || !severity) {
        alert('Please enter both name and severity');
        return;
    }

    if (severity < 1 || severity > 10) {
        alert('Severity must be between 1 and 10');
        return;
    }

    try {
        const response = await fetch(`${API_URL}/add?name=${encodeURIComponent(name)}&severity=${severity}`);
        const result = await response.text();
        alert(result);
        
        // Clear inputs
        document.getElementById('name').value = '';
        document.getElementById('severity').value = '';
        
        // Refresh queue
        await loadQueue();
    } catch (error) {
        alert('Error adding patient: ' + error.message);
    }
}

async function servePatient() {
    try {
        const response = await fetch(`${API_URL}/serve`);
        const result = await response.text();
        alert(result);
        
        // Refresh queue
        await loadQueue();
    } catch (error) {
        alert('Error serving patient: ' + error.message);
    }
}

async function loadQueue() {
    try {
        const response = await fetch(`${API_URL}/list`);
        const patients = await response.json();
        
        const tbody = document.getElementById('queueBody');
        tbody.innerHTML = '';
        
        patients.forEach((patient, index) => {
            const row = tbody.insertRow();
            row.insertCell(0).textContent = patient.patientID;
            row.insertCell(1).textContent = patient.name;
            row.insertCell(2).textContent = patient.severity;
            row.insertCell(3).textContent = patient.arrivalOrder;
        });
    } catch (error) {
        console.error('Error loading queue:', error);
    }
}

// O(1) Patient Lookup Function
async function lookupPatient() {
    const patientId = document.getElementById('patientId').value.trim();
    const resultDiv = document.getElementById('lookupResult');
    
    if (!patientId) {
        resultDiv.innerHTML = '<p style=\"color: red;\">Please enter a Patient ID</p>';
        return;
    }
    
    try {
        const response = await fetch(`${API_URL}/patient/${encodeURIComponent(patientId)}`);
        
        if (response.ok) {
            const patient = await response.json();
            resultDiv.innerHTML = `
                <div style="background: #e8f5e9; padding: 15px; margin-top: 10px; border-radius: 8px; border: 1px solid #4caf50;">
                    <h3 style="margin: 0 0 10px 0; color: #2e7d32;">Patient found!!</h3>
                    <p><strong>Patient ID:</strong> ${patient.patientID}</p>
                    <p><strong>Name:</strong> ${patient.name}</p>
                    <p><strong>Severity:</strong> ${patient.severity}</p>
                    <p><strong>Arrival Order:</strong> ${patient.arrivalOrder}</p>
                </div>
            `;
        } else {
            resultDiv.innerHTML = '<p style=\"color: red; margin-top: 10px;\">Patient not found</p>';
        }
        
        document.getElementById('patientId').value = '';
    } catch (error) {
        resultDiv.innerHTML = '<p style=\"color: red; margin-top: 10px;\">Error looking up patient</p>';
        console.error('Error:', error);
    }
}

// Load queue on page load
window.onload = loadQueue;
