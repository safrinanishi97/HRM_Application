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

  documents: EmployeeDocumentDTO[];
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

  documents: EmployeeDocumentDTO[];
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

  documents: EmployeeDocumentDTO[];
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