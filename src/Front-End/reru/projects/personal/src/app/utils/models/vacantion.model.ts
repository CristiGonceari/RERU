export interface VacantionModel{
    id?: number;
    mentions: string;
    fromDate: string;
    toDate?: string;
    vacationTypeId?: number;
    vacationTypeName?: string;
    institution?: string;
    childAge?: number;
    countDays?: number;
    status?: number;
    vacationRequestId?: number;
    vacationRequestName?: string;
    vacationOrderId?: number;
    vacationOrderName?: string;
    contractorId: number;
    contractorName?: string;
    contractorLastName?: string;
}