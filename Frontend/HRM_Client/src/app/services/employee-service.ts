import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmployeeCreateDTO, EmployeeDTO, EmployeeUpdateDTO } from '../models/employee-dto';
import { catchError, Observable, of } from 'rxjs';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = 'https://localhost:44343/api/employee';
     constructor(private http: HttpClient,
    private sanitizer: DomSanitizer) {}



 getAllEmployees(idClient: number): Observable<EmployeeDTO[]> {
    return this.http.get<EmployeeDTO[]>(`${this.apiUrl}/?idClient=${idClient}`);
  }


    deleteEmployee(idClient: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idClient}/${id}`);
  }

  //   getImageUrl(base64: string): SafeUrl {
  //   return this.sanitizer.bypassSecurityTrustUrl(`data:image/jpeg;base64,${base64}`);
  // }


createEmployee(employee: EmployeeCreateDTO): Observable<any> {
    const formData = this.createFormData(employee);
    return this.http.post(this.apiUrl, formData);
  }

  updateEmployee(employee: EmployeeUpdateDTO): Observable<any> {
    const formData = this.createFormData(employee);
    return this.http.put(this.apiUrl, formData);
  }

  private createFormData(data: any): FormData {
    const formData = new FormData();
    Object.keys(data).forEach(key => {
      if (Array.isArray(data[key])) {
        data[key].forEach((item: any, index: number) => {
          Object.keys(item).forEach(itemKey => {
            if (itemKey === 'UpFile' && item[itemKey]) {
              formData.append(`${key}[${index}].${itemKey}`, item[itemKey]);
            } else {
              formData.append(`${key}[${index}].${itemKey}`, item[itemKey]);
            }
          });
        });
      } else if (key === 'ProfileImage' && data[key]) {
        formData.append(key, data[key]);
      } else {
        formData.append(key, data[key]);
      }
    });
    return formData;
  }






}
