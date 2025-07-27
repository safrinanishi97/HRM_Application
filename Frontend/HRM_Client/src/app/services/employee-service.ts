import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { EmployeeCreateDTO, EmployeeDTO } from '../models/employee-dto';
import { catchError, Observable, of } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class EmployeeService {
    private apiUrl = 'https://localhost:44343/api/employee';
     constructor(private http: HttpClient) {}


// getAllEmployees(idClient: number): Observable<EmployeeDTO[]> {
//   return this.http.get<EmployeeDTO[]>(
//     `${this.apiUrl}/?idClient=${idClient}`
//   ).pipe(
//     catchError(err => {
//       console.error('API Error:', err);
//       return of([]); // Return empty array on error
//     })
//   );
// }
 getAllEmployees(idClient: number): Observable<EmployeeDTO[]> {
    return this.http.get<EmployeeDTO[]>(`${this.apiUrl}/?idClient=${idClient}`);
  }

    deleteEmployee(idClient: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idClient}/${id}`);
  }

  createEmployee(employee: EmployeeCreateDTO): Observable<any> {
  const formData = this.createEmployeeFormData(employee);
  return this.http.post(this.apiUrl, formData);
  }


  private createEmployeeFormData(employee: any): FormData {
    const formData = new FormData();
  

    Object.keys(employee).forEach(key => {
      if (key !== 'Documents' && key !== 'EducationInfos' && key !== 'Certifications' && 
          key !== 'ProfileImage' && key !== 'UpFile') {
        const value = employee[key];
        if (value !== null && value !== undefined) {
          formData.append(key, value);
        }
      }
    });

    if (employee.ProfileImage instanceof File) {
      formData.append('ProfileImage', employee.ProfileImage);
    }

    if (employee.Documents && Array.isArray(employee.Documents)) {
      employee.Documents.forEach((doc: any, index: number) => {
        formData.append(`Documents[${index}].Id`, doc.Id?.toString() || '0');
        formData.append(`Documents[${index}].IdClient`, doc.IdClient?.toString() || '0');
        formData.append(`Documents[${index}].DocumentName`, doc.DocumentName || '');
        formData.append(`Documents[${index}].FileName`, doc.FileName || '');
        formData.append(`Documents[${index}].UploadDate`, doc.UploadDate?.toISOString() || new Date().toISOString());
        formData.append(`Documents[${index}].UploadedFileExtention`, doc.UploadedFileExtention || '');
        
        if (doc.UpFile instanceof File) {
          formData.append(`Documents[${index}].UpFile`, doc.UpFile);
        }
      });
    }

    if (employee.EducationInfos && Array.isArray(employee.EducationInfos)) {
      employee.EducationInfos.forEach((edu: any, index: number) => {
        Object.keys(edu).forEach(key => {
          const value = edu[key];
          if (value !== null && value !== undefined) {
            formData.append(`EducationInfos[${index}].${key}`, value.toString());
          }
        });
      });
    }

    if (employee.Certifications && Array.isArray(employee.Certifications)) {
      employee.Certifications.forEach((cert: any, index: number) => {
        Object.keys(cert).forEach(key => {
          const value = cert[key];
          if (value !== null && value !== undefined) {
            formData.append(`Certifications[${index}].${key}`, value.toString());
          }
        });
      });
    }

    return formData;
  }
}
