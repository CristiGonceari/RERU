export interface ReportModel {
    id?: number;
    type: number;
    name: string;
    contractorId: number;
    contractorName: string;
    departmentId?: number;
    createDate?: string;
}

export interface ReportFilterModel {
    type?: number;
    name?: string;
    contractorName?: string;
    contractorLastName?: string;
    departmentId?: number;
    fromDate?: string;
    toDate?: string;
}
