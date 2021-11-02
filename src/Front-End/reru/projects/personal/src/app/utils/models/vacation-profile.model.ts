export interface VacationProfileModel {
    id?: number;
    mentions: string;
    fromDate: string;
    toDate?: string;
    vacationType: number;
    institution?: string;
    childAge?: number;
    countDays?: number;
    status?: number;
    vacationRequestId?: number;
    vacationRequestName?: string;
    vacationOrderId?: number;
    vacationOrderName?: string;
    contractorName?: string;
    contractorLastName?: string;
}
