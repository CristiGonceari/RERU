export interface NomenclatureColumnModel {
    nomenclatureTypeId: number;
    nomenclatureColumns: NomenclatureColumnsModel[];
}

export interface NomenclatureColumnsModel {
    id?: number;
    name: string;
    type: number;
    isMandatory: boolean;
    order: number;
}
