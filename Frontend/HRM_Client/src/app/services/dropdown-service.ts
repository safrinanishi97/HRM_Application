import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DropdownService {
  
 private apiUrl = 'https://localhost:44343/api/common';
     constructor(private http: HttpClient) {}

getDepartments(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/departmentdropdown`);
}

getDesignations(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/designationdropdown`);
}

getEducationLevels(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/educationleveldropdown`);
}
getEducationResults(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/educationresultdropdown`);
}
getEmployeeTypes(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/employeetypedropdown`);
}

getGenders(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/genderdropdown?idClient=${idClient}`);
}

getJobTypes(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/designationdropdown?idClient=${idClient}`);
}


// getMaritalStatus(): Observable<any[]> {
//   return this.http.get<any[]>(`${this.apiUrl}/departments`);
// }
// getMaritalStatus(): Observable<any[]> {
//   return this.http.get<any[]>(`${this.apiUrl}/departments`);
// }

// getMaritalStatus(): Observable<any[]> {
//   return this.http.get<any[]>(`${this.apiUrl}/departments`);
// }

// getMaritalStatus(): Observable<any[]> {
//   return this.http.get<any[]>(`${this.apiUrl}/departments`);
// }


}
