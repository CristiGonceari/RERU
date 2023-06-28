export interface StudyModel {
    id?: number;
    institution: string;
    studyFrequency: number;
    institutionAddress: string;
    faculty: string;
    specialty: string;
    yearOfAdmission: string;
    graduationYear: string;
    studyTypeId: number;
    contractorId: number
    studyProfile: number,
    studyCourse: number,
    startStudyPeriod: string,
    endStudyPeriod: string,
    title: string,
    qualification: string,
    creditCount: number,
    studyActSeries: string,
    studyActNumber: number,
    studyActRelaseDay: string
}

export interface ContractorStudyModel {
    id?: number;
    institution: string;
    studyFrequency: number;
    institutionAddress: string;
    faculty: string;
    specialty: string;
    yearOfAdmission: Date;
    graduationYear: Date;
    studyTypeId: number;
    contractorId: number
}
