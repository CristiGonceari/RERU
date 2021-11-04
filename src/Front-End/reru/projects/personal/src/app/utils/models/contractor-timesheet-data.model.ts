import { TimesheetDataModel } from "./timesheet-data.model";

export interface ContractorTimesheetDataModel{
    contractorId: number;
    contractorName: string;
    deparmentName: string;
    roleName: string;
    content: TimesheetDataModel[];
    workedHours: number;
    freehours: number;
    workingDays: number;
}

