export interface VacationModel {
    id: number;
    reason: string;
    mentions: string;
    fromDate: string;
    toDate: string;
    contractorId: number;
    contractorName: string;
    vacationTypeId: number;
    vacationTypeName: string;
    vacationFile: any;
    document?: any;
    days?: number;
}
