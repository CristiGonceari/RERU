export interface NomenclatureRecordModel {
    id?: number;
    name: string;
    isActive: boolean;
    nomenclatureTypeId: number;
    recordValues: RecordValuesModel[];
}

export interface RecordValuesModel {
    id?: number;
    nomenclatureRecordId?: number;
    nomenclatureColumnId: number;
    stringValue: string;
    type?: number;
}
