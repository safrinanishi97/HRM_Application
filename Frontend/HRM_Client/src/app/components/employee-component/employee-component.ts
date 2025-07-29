import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { EmployeeCreateDTO, EmployeeDTO, EmployeeUpdateDTO } from '../../models/employee-dto';
import { EmployeeService } from '../../services/employee-service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
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
    private sanitizer: DomSanitizer
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

   loadEmployees(): void {
    this.employeeService.getAllEmployees(this.idClient).subscribe(data => {
      this.employees = data;
    });
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


  selectEmployee(employee: EmployeeDTO): void {
    this.isEditMode = true;
    this.selectedEmployee = employee;
    
    // // Convert base64 image to URL
    //   if (employee.fileBase64) {
    //   this.profileImageUrl = this.employeeService.getImageUrl(employee.fileBase64);
    // } else {
    //   this.profileImageUrl = null;
    // }

    if (employee.fileBase64) {
      this.profileImageUrl = this.sanitizer.bypassSecurityTrustUrl(
        `data:image/jpeg;base64,${employee.fileBase64}`
      );
    } else {
      this.profileImageUrl = null;
    }

    this.employeeForm.patchValue({
      id: employee.id,
      idClient: employee.idClient,
      employeeName: employee.employeeName,
      employeeNameBangla: employee.employeeNameBangla,
      fatherName: employee.fatherName,
      motherName: employee.motherName,
      birthDate: employee.birthDate ,
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
    

    this.clearFormArrays();

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

    employee.familyInfos.forEach(fam => {
      const famGroup = this.fb.group({
        id: [fam.id],
        idClient: [fam.idClient],
        idEmployee: [fam.idEmployee],
        name: [fam.name, Validators.required],
        idGender: [fam.idGender, Validators.required],
        idRelationship: [fam.idRelationship, Validators.required],
        dateOfBirth: [fam.dateOfBirth],
        contactNo: [fam.contactNo],
        currentAddress: [fam.currentAddress],
        permanentAddress: [fam.permanentAddress]
      });
      this.familyInfos.push(famGroup);
    });

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
    return this.employeeForm.get('documents') as FormArray;
  }

  get educationInfos(): FormArray {
    return this.employeeForm.get('educationInfos') as FormArray;
  }

  get familyInfos(): FormArray {
    return this.employeeForm.get('familyInfos') as FormArray;
  }

  get certifications(): FormArray {
    return this.employeeForm.get('certifications') as FormArray;
  }

 onProfileImageChange(event: any): void {
    const file = event.target.files[0];
    if (file) {
      this.employeeForm.patchValue({ profileImage: file });
      this.employeeForm.get('profileImage')?.updateValueAndValidity();
      
      const reader = new FileReader();
      reader.onload = () => {
        this.profileImageUrl = this.sanitizer.bypassSecurityTrustUrl(
          reader.result as string
        );
      };
      reader.readAsDataURL(file);
    }
  }
  addFamilyInfo(): void {
    const famGroup = this.fb.group({
      id: [0],
      idClient: [this.idClient],
      idEmployee: [0],
      name: ['', Validators.required],
      idGender: [0, Validators.required],
      idRelationship: [0, Validators.required],
      dateOfBirth: [null],
      contactNo: [''],
      currentAddress: [''],
      permanentAddress: ['']
    });
    this.familyInfos.push(famGroup);
  }
    removeFamilyInfo(index: number): void {
    this.familyInfos.removeAt(index);
  }

  addEducationInfo(): void {
    const eduGroup = this.fb.group({
      id: [0],
      idClient: [this.idClient],
      idEmployee: [0],
      idEducationLevel: [0, Validators.required],
      idEducationExamination: [0, Validators.required],
      idEducationResult: [0, Validators.required],
      cgpa: [0],
      examScale: [0],
      marks: [0],
      major: ['', Validators.required],
      passingYear: [0, Validators.required],
      instituteName: ['', Validators.required],
      isForeignInstitute: [false],
      duration: [0],
      achievement: ['']
    });
    this.educationInfos.push(eduGroup);
  }

  removeEducationInfo(index: number): void {
    this.educationInfos.removeAt(index);
  }

  addCertification(): void {
    const certGroup = this.fb.group({
      id: [0],
      idClient: [this.idClient],
      idEmployee: [0],
      certificationTitle: ['', Validators.required],
      certificationInstitute: ['', Validators.required],
      instituteLocation: ['', Validators.required],
      fromDate: [new Date(), Validators.required],
      toDate: [null]
    });
    this.certifications.push(certGroup);
  }


  removeCertification(index: number): void {
    this.certifications.removeAt(index);
  }

 addDocument(): void {
    const docGroup = this.fb.group({
      id: [0],
      idClient: [this.idClient],
      idEmployee: [0],
      documentName: ['', Validators.required],
      fileName: ['', Validators.required],
      uploadDate: [new Date(), Validators.required],
      uploadedFileExtention: [''],
      upFile: [null],
      fileBase64: ['']
    });
    this.documents.push(docGroup);
  }

  removeDocument(index: number): void {
    this.documents.removeAt(index);
  }

  onDocumentFileChange(event: any, index: number): void {
    const file = event.target.files[0];
    if (file) {
      const docGroup = this.documents.at(index);
      docGroup.patchValue({ upFile: file });
      docGroup.get('upFile')?.updateValueAndValidity();
      
      const reader = new FileReader();
      reader.onload = () => {
        docGroup.patchValue({ fileBase64: reader.result?.toString().split(',')[1] });
      };
      reader.readAsDataURL(file);
    }
  }

}
