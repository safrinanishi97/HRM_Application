import { CommonModule } from '@angular/common';
import { Component, OnInit } from '@angular/core';
import { FormArray, FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, ValidationErrors, Validators } from '@angular/forms';
import { EmployeeCreateDTO, EmployeeDTO, EmployeeUpdateDTO } from '../../models/employee-dto';
import { EmployeeService } from '../../services/employee-service';
import { DomSanitizer, SafeUrl } from '@angular/platform-browser';
import { DropdownService } from '../../services/dropdown-service';
import { ToastrService } from 'ngx-toastr';


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
  designations: any[] = [];
  educationExaminations: any[] = [];
  educationLevels: any[] = [];
  educationResults: any[] = [];
  employeeTypes: any[] = [];
  genders: any[] = [];
  jobTypes: any[] = [];
  maritalStatus: any[] = [];
  relationship: any[] = [];
  religions: any[] = [];
  sections: any[] = [];
  weekOffs: any[] = [];

  isCreating = false;
  isEditing = false;
  private formatDate(date: Date | string): string {
  const d = new Date(date);
  let month = '' + (d.getMonth() + 1);
  let day = '' + d.getDate();
  const year = d.getFullYear();
  if (month.length < 2) month = '0' + month;
  if (day.length < 2) day = '0' + day;
  return [year, month, day].join('-');
}
constructor(
    private employeeService: EmployeeService,
    private dropdownService: DropdownService,
    private fb: FormBuilder,
    private sanitizer: DomSanitizer,
    private toastr: ToastrService
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
  idDepartment: [null, Validators.required],
  departmentName: [''],
  idSection: [null, Validators.required],
  sectionName: [''],
  idDesignation: [null],
  designation: [''],
  address: [''],
  idGender: [null],
  genderName: [''],
  idReligion: [null],
  religionName: [''],
  idReportingManager: [0],
  reportingManager: [''],
  idJobType: [null],
  jobTypeName: [''],
  idEmployeeType: [null],
  typeName: [''],
  presentAddress: [''],
  nationalIdentificationNumber: [''],
  contactNo: [''],
  isActive: [true],
  hasOvertime: [false],
  hasAttendenceBonus: [false],
  idWeekOff: [null],
  weekOffDay: [''],
  idMaritalStatus: [null],
  maritalStatusName: [''],
  setDate: [this.formatDate(new Date())] ,
  createdBy: [''],
  profileImage: [null],
  fileBase64: [''],
  documents: this.fb.array([]),
  educationInfos: this.fb.array([]),
  familyInfos: this.fb.array([]),
  certifications: this.fb.array([])
  });
    
  }


  ngOnInit(): void {
    this.employeeForm.disable(); 
    this.loadDropdownData();
    this.loadEmployees();
  }

   loadEmployees(): void {
    this.employeeService.getAllEmployees(this.idClient).subscribe(data => {
      this.employees = data;
    });
  }

 loadDropdownData(): void {
    // const idClient = 10001001;
    this.dropdownService.getDepartments(this.idClient).subscribe({
      next: (data) => this.departments = data,
      error: (err) => console.error('Failed to load departments', err)
      
    });

    this.dropdownService.getDesignations(this.idClient).subscribe({
      next: (data) => this.designations = data,
      error: (err) => console.error('Failed to load sections', err)
    });
    this.dropdownService.getEducationExaminations(this.idClient).subscribe({
      next: (data) => this.educationExaminations = data,
      error: (err) => console.error('Failed to load designations', err)
    });

    this.dropdownService.getEducationLevels(this.idClient).subscribe({
      next: (data) => this.educationLevels = data,
      error: (err) => console.error('Failed to load designations', err)
    });

    this.dropdownService.getEducationResults(this.idClient).subscribe({
      next: (data) => this.educationResults = data,
      error: (err) => console.error('Failed to load genders', err)
    });

    this.dropdownService.getEmployeeTypes(this.idClient).subscribe({
      next: (data) => this.employeeTypes = data,
      error: (err) => console.error('Failed to load religions', err)
    });
    
    this.dropdownService.getGenders(this.idClient).subscribe({
      next: (data) => this.genders = data,
      error: (err) => console.error('Failed to load religions', err)
    });
    
    this.dropdownService.getJobTypes(this.idClient).subscribe({
      next: (data) => this.jobTypes = data,
      error: (err) => console.error('Failed to load religions', err)
    });
    
    this.dropdownService.getMaritalStatus(this.idClient).subscribe({
      next: (data) => this.maritalStatus = data,
      error: (err) => console.error('Failed to load religions', err)
    });
    
    this.dropdownService.getRelationship(this.idClient).subscribe({
      next: (data) => this.relationship = data,
      error: (err) => console.error('Failed to load religions', err)
    });
    
    this.dropdownService.getReligions(this.idClient).subscribe({
      next: (data) => this.religions = data,
      error: (err) => console.error('Failed to load religions', err)
    });
      this.dropdownService.getSections(this.idClient).subscribe({
      next: (data) => this.sections = data,
      error: (err) => console.error('Failed to load religions', err)
    });
      this.dropdownService.getWeekOffs(this.idClient).subscribe({
      next: (data) => this.weekOffs = data,
      error: (err) => console.error('Failed to load religions', err)
    });
  }

selectEmployee(employeeId: number): void {
  this.employeeService.getEmployeeById(this.idClient, employeeId).subscribe({
    next: (employee) => {
      this.isEditMode = true;
      this.selectedEmployee = employee;

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
        birthDate: employee.birthDate ? this.formatDate(employee.birthDate) : null,
        joiningDate: employee.joiningDate ? this.formatDate(employee.joiningDate) : null,
        idDepartment: employee.idDepartment,
        idSection: employee.idSection,
        idDesignation: employee.idDesignation,
        idGender: employee.idGender,
        idReligion: employee.idReligion,
        idReportingManager: employee.idReportingManager,
        reportingManager: employee.reportingManager,
        idJobType: employee.idJobType,
        jobTypeName: employee.jobTypeName,
        idEmployeeType: employee.idEmployeeType,
        typeName: employee.typeName,
        address: employee.address,
        presentAddress: employee.presentAddress,
        nationalIdentificationNumber: employee.nationalIdentificationNumber,
        contactNo: employee.contactNo,
        isActive: employee.isActive,
        hasOvertime: employee.hasOvertime,
        hasAttendenceBonus: employee.hasAttendenceBonus,
        idWeekOff: employee.idWeekOff,
        weekOffDay: employee.weekOffDay,
        idMaritalStatus: employee.idMaritalStatus,
        maritalStatusName: employee.maritalStatusName,
        setDate: employee.setDate,
        createdBy: employee.createdBy,
        profileImage: null
      });
      this.employeeForm.disable();
      this.isEditMode = true;
      this.clearFormArrays();

      employee.documents.forEach(doc => {
        const docGroup = this.fb.group({
          id: [doc.id],
          idClient: [doc.idClient],
          idEmployee: [doc.idEmployee],
          documentName: [doc.documentName, Validators.required],
          fileName: [doc.fileName, Validators.required],
          uploadDate: [doc.uploadDate ? this.formatDate(doc.uploadDate) : null, Validators.required],
          uploadedFileExtention: [doc.uploadedFileExtention],
          upFile: [null],
          fileBase64: [doc.fileBase64],
          setDate: [doc.setDate ? this.formatDate(doc.setDate) : null],
          createdBy: [doc.createdBy]
        });
        this.documents.push(docGroup);
      });

     employee.educationInfos.forEach(edu => {
      const eduGroup = this.fb.group({
        id: [edu.id],
        idClient: [edu.idClient],
        idEmployee: [edu.idEmployee],
        idEducationLevel: [edu.idEducationLevel, Validators.required],
        educationLevelName: [edu.educationLevelName],
        idEducationExamination: [edu.idEducationExamination, Validators.required],
        examName: [edu.examName],
        idEducationResult: [edu.idEducationResult, Validators.required],
        resultName: [edu.resultName],
        cgpa: [edu.cgpa],
        examScale: [edu.examScale],
        marks: [edu.marks],
        major: [edu.major, Validators.required],
        passingYear: [edu.passingYear, Validators.required],
        instituteName: [edu.instituteName, Validators.required],
        isForeignInstitute: [edu.isForeignInstitute],
        duration: [edu.duration],
        achievement: [edu.achievement],
        setDate: [edu.setDate ? this.formatDate(edu.setDate) : null],
        createdBy: [edu.createdBy]
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
        genderName: [fam.genderName],
        idRelationship: [fam.idRelationship, Validators.required],
        relationName: [fam.relationName],
        dateOfBirth: [fam.dateOfBirth ? this.formatDate(fam.dateOfBirth) : null],
        contactNo: [fam.contactNo],
        currentAddress: [fam.currentAddress],
        permanentAddress: [fam.permanentAddress],
        setDate: [fam.setDate ? this.formatDate(fam.setDate) : null],
        createdBy: [fam.createdBy]
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
    fromDate: [cert.fromDate ? this.formatDate(cert.fromDate) : null, Validators.required],
    toDate: [cert.toDate ? this.formatDate(cert.toDate) : null],
    setDate: [cert.setDate ? this.formatDate(cert.setDate) : null],
    createdBy: [cert.createdBy]
  });
  this.certifications.push(certGroup);
});
    },
    error: (err) => {
      console.error('Failed to load employee', err);
    }
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
      idGender: [null, Validators.required],
      idRelationship: [null, Validators.required],
      dateOfBirth: [null],
      contactNo: [''],
      currentAddress: [''],
      permanentAddress: ['']
    });
        console.log('Adding family info group:', famGroup.value);
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
      idEducationLevel: [null, Validators.required],
      idEducationExamination: [null, Validators.required],
      idEducationResult: [null, Validators.required],
      cgpa: [0],
      examScale: [0],
      marks: [0],
      major: ['', Validators.required],
      passingYear: [0, Validators.required],
      instituteName: ['', Validators.required],
      isForeignInstitute: [false, Validators.required],
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
      fromDate: [null, Validators.required],
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
      docGroup.patchValue({ 
        upFile: file,
        fileName: file.name,
        uploadedFileExtention: file.name.split('.').pop()
      });
      docGroup.get('upFile')?.updateValueAndValidity();
      
      const reader = new FileReader();
      reader.onload = () => {
        docGroup.patchValue({ fileBase64: reader.result?.toString().split(',')[1] });
      };
      reader.readAsDataURL(file);
    }
  }

  // getDocumentPreviewUrl(document: any): SafeUrl | null {
  //   if (document.fileBase64) {
  //     return this.sanitizer.bypassSecurityTrustUrl(
  //       `data:image/jpeg;base64,${document.fileBase64}`
  //     );
  //   }
  //   console.log('Preview URL:', this.getDocumentPreviewUrl(document.value));

  //   return null;
  // }
  


  startCreateMode(): void {
  this.employeeForm.reset({
    id: 0,
    idClient: this.idClient,
    isActive: true
  });

  this.clearFormArrays(); 
  this.employeeForm.enable(); 
  this.isCreating = true; 
  this.isEditMode = false;
  this.isEditing = false;
  this.profileImageUrl = null;

  console.log('Starting create mode', this.employeeForm);
}

 
createEmployee(): void {
  if (this.employeeForm.invalid) {
    //this.markFormGroupTouched(this.employeeForm);
    return;
  }

  const formData = this.employeeForm.getRawValue() as EmployeeCreateDTO;

  // formData.educationInfos = formData.educationInfos.filter(e =>
  //   e.idEducationLevel && e.idEducationExamination && e.idEducationResult && e.major && e.passingYear && e.instituteName
  // );

  // formData.certifications = formData.certifications.filter(c =>
  //   c.certificationTitle && c.certificationInstitute && c.instituteLocation && c.fromDate
  // );

  // formData.familyInfos = formData.familyInfos.filter(f =>
  //   f.name && f.idGender && f.idRelationship
  // );

  // formData.documents = formData.documents.filter(d =>
  //   d.documentName && d.fileName && d.uploadDate
  // );

  this.employeeService.createEmployee(formData).subscribe({
    next: () => {
      this.loadEmployees();
      this.resetForm();
      this.isCreating = false;
      this.toastr.success('Employee created successfully!', 'Success');
    },
    error: (err) => {
      // console.error('Full error:', err);
      this.toastr.error('Failed to create employee', 'Error');
    }
  });
}



  updateEmployee(): void {
    if (this.employeeForm.invalid) return;
    const formData = this.employeeForm.value as EmployeeUpdateDTO;
    console.log('Form Data:', formData);
    this.employeeService.updateEmployee(formData).subscribe({
      next: () => {
        this.loadEmployees();
        this.resetForm();
        this.toastr.success('Employee updated successfully!', 'Success');
      },
      error: (err) =>{
        this.toastr.error('Failed to update employee', 'Error');
      }
    });
  }
  enableFormEditing(): void {
  this.employeeForm.enable();
   this.isEditing = true; 
}

  deleteEmployee(): void {
    if (!this.selectedEmployee) return;

    if (confirm('Are you sure to delete this employee?')) {
      this.employeeService.deleteEmployee(
        this.selectedEmployee.idClient, 
        this.selectedEmployee.id
      ).subscribe({
        next: () => {
          this.loadEmployees();
        this.resetForm();
        this.toastr.success('Employee deleted successfully!', 'Success');
        },
        error: (err) => console.error(err)
      });
    }
  }
resetForm(): void {
  this.employeeForm.reset({
    id: 0,
    idClient: this.idClient,
    isActive: true
  });
  this.employeeForm.disable();
  this.clearFormArrays();

  this.selectedEmployee = null;
  this.isEditMode = false;
  this.isEditing = false;
  this.isCreating = false;
  this.profileImageUrl = null;
}

}
