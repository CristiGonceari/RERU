
export interface StudyModel {
    id?: number;
    institution: string;
    studyFrequency: number;
    institutionAddress: string;
    faculty: string;
    specialty: string;
    yearOfAdmission: Date;
    graduationYear: Date;
    studyTypeId: number;
    userProfileId: number
}
