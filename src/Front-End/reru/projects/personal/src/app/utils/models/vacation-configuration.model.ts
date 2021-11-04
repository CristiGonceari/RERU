export interface VacationConfigurationModel {
    paidLeaveDays: number;
    nonPaidLeaveDays: number;
    studyLeaveDays: number;
    deathLeaveDays: number;
    childCareLeaveDays: number;
    childBirthLeaveDays: number;
    marriageLeaveDays: number;
    paternalistLeaveDays: number;
    includeOffDays: boolean;
    includeHolidayDays: boolean;
    mondayIsWorkDay: boolean;
    tuesdayIsWorkDay: boolean;
    wednesdayIsWorkDay: boolean;
    thursdayIsWorkDay: boolean;
    fridayIsWorkDay: boolean;
    saturdayIsWorkDay: boolean;
    sundayIsWorkDay: boolean;
}
