import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class DropdownService {
  
 private apiUrl = 'https://localhost:44343/api/common';
     constructor(private http: HttpClient) {}

getDepartments(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/departmentdropdown?idClient=${idClient}`);
}

getDesignations(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/designationdropdown?idClient=${idClient}`);
}

getEducationLevels(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/educationleveldropdown?idClient=${idClient}`);
}
getEducationResults(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/educationresultdropdown?idClient=${idClient}`);
}
getEmployeeTypes(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/employeetypedropdown?idClient=${idClient}`);
}

getGenders(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/genderdropdown?idClient=${idClient}`);
}

getJobTypes(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/jobtypedropdown?idClient=${idClient}`);
}

getMaritalStatus(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/maritalstatusdropdown?idClient=${idClient}`);
}
getRelationship(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/relationshipdropdown?idClient=${idClient}`);
}

getReligions(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/religiondropdown?idClient=${idClient}`);
}

getSections(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/sectiondropdown?idClient=${idClient}`);
}

getWeekOffs(idClient: number): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/weeksoffdropdown?idClient=${idClient}`);
}

}
