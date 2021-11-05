import { StudyFrequencyEnum } from "./study-frequency.enum";

export interface StudyModel {
    id?: number;
    institution: string;
    studyFrequency: StudyFrequencyEnum;
    faculty: string;
    qualification: string;
    specialty: string;
    diplomaNumber: string;
    diplomaReleaseDay: string;
    isActiveStudy: boolean;
    contractorId?: number;
    studyTypeId: number;
}
