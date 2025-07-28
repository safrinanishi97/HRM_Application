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
  idSection: number;
  address?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  isActive?: boolean;
  profileImage?: File;
  fileBase64?: string;
  idGender?: number;
  idReligion?: number;
  idDesignation?: number;
  departmentName: string;
  designation?: string;
  sectionName: string;
  gender?: string;
  documents: EmployeeDocumentDTO[];
  familyInfos: EmployeeFamilyInfoDTO[];
  educationInfos: EmployeeEducationInfoDTO[];
  certifications: EmployeeProfessionalCertificationDTO[];
}


export interface EmployeeCreateDTO {
  idClient: number;
  employeeName?: string;
  employeeNameBangla?: string;
  fatherName?: string;
  motherName?: string;
  birthDate?: Date;
  joiningDate?: Date;
  idDepartment: number;
  idSection: number;
  address?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  ProfileImage?: File | null;
  idGender?: number;
  idReligion?: number;
  idDesignation?: number;
  departmentName: string;
  designation?: string;
  sectionName: string;
  gender?: string;
  documents: EmployeeDocumentDTO[];
  familyInfos: EmployeeFamilyInfoDTO[];
  educationInfos: EmployeeEducationInfoDTO[];
  certifications: EmployeeProfessionalCertificationDTO[];
}


export interface EmployeeUpdateDTO {
  id: number;
  idClient: number;
  employeeName?: string;
  employeeNameBangla?: string;
  fatherName?: string;
  motherName?: string;
  birthDate?: Date;
  joiningDate: Date;
  idDepartment: number;
  idSection: number;
  address?: string;
  presentAddress?: string;
  nationalIdentificationNumber?: string;
  contactNo?: string;
  isActive?: boolean;
  isForeignInstitute: boolean;
  ProfileImage?: File | null;
  idGender?: number;
  idReligion?: number;
  idDesignation?: number;
  departmentName: string;
  designation?: string;
  sectionName: string;
  gender?: string;
  documents: EmployeeDocumentDTO[];
  familyInfos: EmployeeFamilyInfoDTO[];
  educationInfos: EmployeeEducationInfoDTO[];
  certifications: EmployeeProfessionalCertificationDTO[];
}

export interface EmployeeDocumentDTO {
  id: number;
  idClient: number;
  idEmployee: number;
  documentName: string;
  fileName: string;
  uploadDate: Date;
  uploadedFileExtention?: string;
  UpFile?: File | null;
  fileBase64?: string;
}


export interface EmployeeFamilyInfoDTO {
  idClient: number;
  id: number;
  idEmployee: number;
  name: string;
  idGender: number;
  idRelationship: number;
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
  idEducationExamination: number;
  idEducationResult: number;
  cgpa?: number;
  examScale?: number;
  marks?: number;
  major: string;
  passingYear: number;
  instituteName: string;
  isForeignInstitute: boolean;
  duration?: number;
  achievement?: string;
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
}

export interface DropdownItem {
  id: number;
  text: string;
}
