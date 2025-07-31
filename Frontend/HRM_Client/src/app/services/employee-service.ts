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
  getEmployeeById(idClient: number, id: number): Observable<EmployeeDTO> {
    return this.http.get<EmployeeDTO>(`${this.apiUrl}/getbyid?idClient=${idClient}&id=${id}`);
  }

    deleteEmployee(idClient: number, id: number): Observable<any> {
    return this.http.delete(`${this.apiUrl}/${idClient}/${id}`);
  }

createEmployee(employee: EmployeeCreateDTO): Observable<any> {
    const formData = this.createFormData(employee);
    return this.http.post(this.apiUrl, formData);
  }

  updateEmployee(employee: EmployeeUpdateDTO): Observable<any> {
    const formData = this.createFormData(employee);
    return this.http.put(this.apiUrl, formData);
  }

private createFormData(data: any, isUpdate: boolean = false): FormData {
  const formData = new FormData();

  Object.keys(data).forEach(key => {
    const value = data[key];

    if (key === 'profileImage' || Array.isArray(value)) {
      return;
    }
    if (value instanceof Date) {
      formData.append(key, value.toISOString());
    }
    else if (value !== null && value !== undefined) {
      formData.append(key, value);
    }
  });

  if (data.profileImage instanceof File) {
    formData.append('profileImage', data.profileImage);
  }

  if (isUpdate) {
    this.appendArrayForUpdate(formData, 'documents', data.documents);
    this.appendArrayForUpdate(formData, 'educationInfos', data.educationInfos);
    this.appendArrayForUpdate(formData, 'certifications', data.certifications);
    this.appendArrayForUpdate(formData, 'familyInfos', data.familyInfos);
  } else {
    this.appendArrayIfNotEmpty(formData, 'documents', data.documents);
    this.appendArrayIfNotEmpty(formData, 'educationInfos', data.educationInfos);
    this.appendArrayIfNotEmpty(formData, 'certifications', data.certifications);
    this.appendArrayIfNotEmpty(formData, 'familyInfos', data.familyInfos);
  }

  return formData;
}

private appendArrayIfNotEmpty(formData: FormData, arrayName: string, array: any[] | null | undefined) {
  if (!array || array.length === 0) {
    return;
  }
  this.appendArrayItems(formData, arrayName, array);
}

private appendArrayForUpdate(formData: FormData, arrayName: string, array: any[] | null | undefined) {

  const items = array || [];
  this.appendArrayItems(formData, arrayName, items);
}

private appendArrayItems(formData: FormData, arrayName: string, array: any[]) {
  array.forEach((item, index) => {
    if (item && typeof item === 'object') {
      Object.keys(item).forEach(key => {
        const value = item[key];

        if (key === 'upFile' && value instanceof File) {
          formData.append(`${arrayName}[${index}].upFile`, value);
        }
        else if (value instanceof Date) {
          formData.append(`${arrayName}[${index}].${key}`, value.toISOString());
        }
        else if (value !== null && value !== undefined) {
          formData.append(`${arrayName}[${index}].${key}`, value);
        }
      });
    }
  });
}
}
