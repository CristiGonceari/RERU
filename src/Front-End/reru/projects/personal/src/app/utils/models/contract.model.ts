export interface ContractModel {
    id?: number;
    no?: string;
    superiorId?: number;
    netSalary: number;
    brutSalary: number;
    vacationDays: number;
    currencyTypeId: number;
    contractorId?:number;
    instruction?:InstructionModel;
    instructions?: InstructionModel[];
}

export interface InstructionModel {
    id?: number;
    thematic: string;
    instructorName: string;
    instructorLastName: string;
    duration: number;
    date: string;
    contractorId?: number;
}
