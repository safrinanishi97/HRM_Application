export interface EmployeeDTO {
  id: number;
  idClient: number;
  employeeName?: string;
  employeeNameBangla?: string;
  fatherName?: string;
  motherName?: string;
  birthDate?: Date;
  joiningDate?: Date;
  idDepartment: number;
  departmentName?: string;
  idSection: number;
  sectionName?: string;
  idDesignation?: number;
  designation?: string;
  address?: string;
  idGender?: number;
  genderName?: string;
  idReligion?: number;
  religionName?: string;
  idReportingManager?: number;
  reportingManager?: string;
  idJobType?: number;
  jobTypeName?: string;
  idEmployeeType?: number;
  typeName?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  isActive?: boolean;
  hasOvertime?: boolean;
  hasAttendenceBonus?: boolean;
  idWeekOff?: number;
  weekOffDay?: string;
  idMaritalStatus?: number;
  maritalStatusName?: string;
  setDate?: Date;
  createdBy?: string;
  documents: EmployeeDocumentDTO[];
  educationInfos: EmployeeEducationInfoDTO[];
  certifications: EmployeeProfessionalCertificationDTO[];
  familyInfos: EmployeeFamilyInfoDTO[];
  profileImage?: File;
  fileBase64?: string;
}


export interface EmployeeCreateDTO {
  id: number;
  idClient: number;
  employeeName?: string;
  employeeNameBangla?: string;
  fatherName?: string;
  motherName?: string;
  birthDate?: Date;
  joiningDate?: Date;
  idDepartment: number;
  departmentName?: string;
  idSection: number;
  sectionName?: string;
  idDesignation?: number;
  designation?: string;
  address?: string;
  idGender?: number;
  genderName?: string;
  idReligion?: number;
  religionName?: string;
  idReportingManager?: number;
  reportingManager?: string;
  idJobType?: number;
  jobTypeName?: string;
  idEmployeeType?: number;
  typeName?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  isActive?: boolean;
  hasOvertime?: boolean;
  hasAttendenceBonus?: boolean;
  idWeekOff?: number;
  weekOffDay?: string;
  idMaritalStatus?: number;
  maritalStatusName?: string;
  setDate?: Date;
  createdBy?: string;
 documents: EmployeeDocumentDTO[];
  educationInfos: EmployeeEducationInfoDTO[];
  certifications: EmployeeProfessionalCertificationDTO[];
  familyInfos: EmployeeFamilyInfoDTO[];
  profileImage?: File;
}


export interface EmployeeUpdateDTO {
   id: number;
  idClient: number;
  employeeName?: string;
  employeeNameBangla?: string;
  fatherName?: string;
  motherName?: string;
  birthDate?: Date;
  joiningDate?: Date;
  idDepartment: number;
  departmentName?: string;
  idSection: number;
  sectionName?: string;
  idDesignation?: number;
  designation?: string;
  address?: string;
  idGender?: number;
  genderName?: string;
  idReligion?: number;
  religionName?: string;
  idReportingManager?: number;
  reportingManager?: string;
  idJobType?: number;
  jobTypeName?: string;
  idEmployeeType?: number;
  typeName?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  isActive?: boolean;
  hasOvertime?: boolean;
  hasAttendenceBonus?: boolean;
  idWeekOff?: number;
  weekOffDay?: string;
  idMaritalStatus?: number;
  maritalStatusName?: string;
  setDate?: Date;
  createdBy?: string;
  documents: EmployeeDocumentDTO[];
  educationInfos: EmployeeEducationInfoDTO[];
  certifications: EmployeeProfessionalCertificationDTO[];
  familyInfos: EmployeeFamilyInfoDTO[];
  profileImage?: File;
}

export interface EmployeeDocumentDTO {
   id: number;
  idClient: number;
  idEmployee: number;
  documentName: string;
  fileName: string;
  uploadDate: Date;
  uploadedFileExtention?: string;
  upFile?: File;
  fileBase64?: string;
  setDate?: Date;
  createdBy?: string;
}


export interface EmployeeFamilyInfoDTO {
  id: number;
  idClient: number;
  idEmployee: number;
  name: string;
  idGender: number;
  genderName?: string;
  idRelationship: number;
  relationName?: string;
  dateOfBirth?: Date;
  contactNo?: string;
  currentAddress?: string;
  permanentAddress?: string;
  setDate?: Date;
  createdBy?: string;
}
export interface EmployeeEducationInfoDTO {
  id: number;
  idClient: number;
  idEmployee: number;
  idEducationLevel: number;
  educationLevelName?: string;
  idEducationExamination: number;
  examName?: string;
  idEducationResult: number;
  resultName?: string;
  cgpa?: number;
  examScale?: number;
  marks?: number;
  major: string;
  passingYear: number;
  instituteName: string;
  isForeignInstitute: boolean;
  duration?: number;
  achievement?: string;
  setDate?: Date;
  createdBy?: string;
}



export interface EmployeeProfessionalCertificationDTO {
  id: number;
  idClient: number;
  idEmployee: number;
  certificationTitle: string;
  certificationInstitute: string;
  instituteLocation: string;
  fromDate: Date;
  toDate?: Date;
  setDate?: Date;
  createdBy?: string;
}

export interface DropdownItem {
  id: number;
  text: string;
}
