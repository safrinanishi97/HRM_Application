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

getSections(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/departments`);
}

getDesignations(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/designationdropdown`);
}

getGenders(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/genderdropdown`);
}

getReligions(): Observable<any[]> {
  return this.http.get<any[]>(`${this.apiUrl}/religiondropdown`);
}




}
