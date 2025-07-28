import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeDTO } from '../../models/employee-dto';
import { EmployeeService } from '../../services/employee-service';
import { SafeUrl } from '@angular/platform-browser';
import { DropdownService } from '../../services/dropdown-service';

@Component({
  selector: 'app-employee-component',
  imports: [CommonModule,FormsModule,ReactiveFormsModule],
  templateUrl: './employee-component.html',
  styleUrl: './employee-component.css'
})
export class EmployeeComponent implements OnInit {

  employees: EmployeeDTO[] = [];
  selectedEmployee: EmployeeDTO | null = null;
  employeeForm: FormGroup;
  isEditMode = false;
  idClient = 10001001; 
  profileImageUrl: SafeUrl | null = null;
  departments: any[] = [];
  sections: any[] = [];
  designations: any[] = [];
  genders: any[] = [];
  religions: any[] = [];

constructor(
    private employeeService: EmployeeService,
      private dropdownService: DropdownService,
    private fb: FormBuilder,
  ){
    this.employeeForm = this.fb.group({
    id: [0],
    idClient: [this.idClient],
    employeeName: ['', Validators.required],
    employeeNameBangla: [''],
    fatherName: [''],
    motherName: [''],
    birthDate: [null],
    joiningDate: [null],
    idDepartment: [0, Validators.required],
    idSection: [0, Validators.required],
    idDesignation: [null],
    idGender: [null],
    idReligion: [null],
    contactNo: [''],
    departmentName: [''],
    nationalIdentificationNumber: [''],
    address: [''],
    presentAddress: [''],
    isActive: [true],
    profileImage: [null],
    documents: this.fb.array([]),
    educationInfos: this.fb.array([]),
    familyInfos: this.fb.array([]),
    certifications: this.fb.array([])
    });
  }


  ngOnInit(): void {
    this.loadDropdownData();
    this.loadEmployees();
  }

 loadDropdownData(): void {

    this.dropdownService.getDepartments().subscribe({
      next: (data) => this.departments = data,
      error: (err) => console.error('Failed to load departments', err)
    });

    this.dropdownService.getSections().subscribe({
      next: (data) => this.sections = data,
      error: (err) => console.error('Failed to load sections', err)
    });

    this.dropdownService.getDesignations().subscribe({
      next: (data) => this.designations = data,
      error: (err) => console.error('Failed to load designations', err)
    });

    this.dropdownService.getGenders().subscribe({
      next: (data) => this.genders = data,
      error: (err) => console.error('Failed to load genders', err)
    });

    this.dropdownService.getReligions().subscribe({
      next: (data) => this.religions = data,
      error: (err) => console.error('Failed to load religions', err)
    });
  }

   loadEmployees(): void {
    this.employeeService.getAllEmployees(this.idClient).subscribe(data => {
      this.employees = data;
    });
  }

  selectEmployee(employee: EmployeeDTO): void {
    this.isEditMode = true;
    this.selectedEmployee = employee;
    
    // Convert base64 image to URL
      if (employee.fileBase64) {
      this.profileImageUrl = this.employeeService.getImageUrl(employee.fileBase64);
    } else {
      this.profileImageUrl = null;
    }

    // Patch form values
    this.employeeForm.patchValue({
      id: employee.id,
      idClient: employee.idClient,
      employeeName: employee.employeeName,
      employeeNameBangla: employee.employeeNameBangla,
      fatherName: employee.fatherName,
      motherName: employee.motherName,
      birthDate: employee.birthDate,
      joiningDate: employee.joiningDate,
      idDepartment: employee.idDepartment,
      idSection: employee.idSection,
      address: employee.address,
      presentAddress: employee.presentAddress,
      nationalIdentificationNumber: employee.nationalIdentificationNumber,
      contactNo: employee.contactNo,
      isActive: employee.isActive,
      profileImage: null
    });

    // Clear all form arrays
    this.clearFormArrays();

    // Add documents
    employee.documents.forEach(doc => {
      const docGroup = this.fb.group({
        id: [doc.id],
        idClient: [doc.idClient],
        idEmployee: [doc.idEmployee],
        documentName: [doc.documentName, Validators.required],
        fileName: [doc.fileName, Validators.required],
        uploadDate: [doc.uploadDate, Validators.required],
        uploadedFileExtention: [doc.uploadedFileExtention],
        upFile: [null],
        fileBase64: [doc.fileBase64]
      });
      this.documents.push(docGroup);
    });

    // Add education infos
    employee.educationInfos.forEach(edu => {
      const eduGroup = this.fb.group({
        id: [edu.id],
        idClient: [edu.idClient],
        idEmployee: [edu.idEmployee],
        idEducationLevel: [edu.idEducationLevel, Validators.required],
        idEducationExamination: [edu.idEducationExamination, Validators.required],
        idEducationResult: [edu.idEducationResult, Validators.required],
        cgpa: [edu.cgpa],
        examScale: [edu.examScale],
        marks: [edu.marks],
        major: [edu.major, Validators.required],
        passingYear: [edu.passingYear, Validators.required],
        instituteName: [edu.instituteName, Validators.required],
        isForeignInstitute: [edu.isForeignInstitute],
        duration: [edu.duration],
        achievement: [edu.achievement]
      });
      this.educationInfos.push(eduGroup);
    });

    // Add family infos
    employee.familyInfos.forEach(fam => {
      const famGroup = this.fb.group({
        Id: [fam.id],
        IdClient: [fam.idClient],
        IdEmployee: [fam.idEmployee],
        Name: [fam.name, Validators.required],
        IdGender: [fam.idGender, Validators.required],
        IdRelationship: [fam.idRelationship, Validators.required],
        DateOfBirth: [fam.dateOfBirth],
        ContactNo: [fam.contactNo],
        CurrentAddress: [fam.currentAddress],
        PermanentAddress: [fam.permanentAddress]
      });
      this.familyInfos.push(famGroup);
    });

    // Add certifications
    employee.certifications.forEach(cert => {
      const certGroup = this.fb.group({
        id: [cert.id],
        idClient: [cert.idClient],
        idEmployee: [cert.idEmployee],
        certificationTitle: [cert.certificationTitle, Validators.required],
        certificationInstitute: [cert.certificationInstitute, Validators.required],
        instituteLocation: [cert.instituteLocation, Validators.required],
        fromDate: [cert.fromDate, Validators.required],
        toDate: [cert.toDate]
      });
      this.certifications.push(certGroup);
    });
  }

clearFormArrays(): void {
    while (this.documents.length !== 0) {
      this.documents.removeAt(0);
    }
    while (this.educationInfos.length !== 0) {
      this.educationInfos.removeAt(0);
    }
    while (this.familyInfos.length !== 0) {
      this.familyInfos.removeAt(0);
    }
    while (this.certifications.length !== 0) {
      this.certifications.removeAt(0);
    }
  }

  get documents(): FormArray {
    return this.employeeForm.get('Documents') as FormArray;
  }

  get educationInfos(): FormArray {
    return this.employeeForm.get('EducationInfos') as FormArray;
  }

  get familyInfos(): FormArray {
    return this.employeeForm.get('FamilyInfos') as FormArray;
  }

  get certifications(): FormArray {
    return this.employeeForm.get('Certifications') as FormArray;
  }
  
}
