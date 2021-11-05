export interface TimesheetDataModel{
    id: number;
    contractorId: number;
    date: string;
    // nomenclatureRecordId: number;
    // nomenclatureRecordValue: string;
}

export interface AddEditTimeSheetTableModel{
    contractorId: number;
    date: string;
    //nomenclatureRecordId: number;
}

export interface ProfileTimeSheetTableModel{
    contractorName: string;
    content: TimesheetDataModel[];
    workedHours: number;
    freehours: number;
}