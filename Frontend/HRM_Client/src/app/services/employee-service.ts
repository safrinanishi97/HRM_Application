import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmployeeDTO } from '../models/employee-dto';
import { catchError, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = 'https://localhost:44343/api/employee';
     constructor(private http: HttpClient) {}


getAllEmployees(idClient: number): Observable<EmployeeDTO[]> {
  return this.http.get<EmployeeDTO[]>(
    `${this.apiUrl}/?idClient=${idClient}`
  ).pipe(
    catchError(err => {
      console.error('API Error:', err);
      return of([]); // Return empty array on error
    })
  );
}

    deleteEmployee(idClient: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idClient}/${id}`);
  }
}
