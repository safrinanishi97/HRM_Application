import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmployeeCreateDTO, EmployeeDTO } from '../models/employee-dto';
import { catchError, Observable, of } from 'rxjs';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = 'https://localhost:44343/api/employee';
     constructor(private http: HttpClient, private sanitizer: DomSanitizer) {}



 getAllEmployees(idClient: number): Observable<EmployeeDTO[]> {
    return this.http.get<EmployeeDTO[]>(`${this.apiUrl}/?idClient=${idClient}`);
  }


    deleteEmployee(idClient: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idClient}/${id}`);
  }

  getImageUrl(base64: string): SafeUrl {
    return this.sanitizer.bypassSecurityTrustUrl(`data:image/jpeg;base64,${base64}`);
  }
  














}
