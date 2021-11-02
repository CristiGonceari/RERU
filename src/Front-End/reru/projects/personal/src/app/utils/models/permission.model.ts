export interface PermissionModel {
    contractorId?: number;

    getGeneralData: boolean;
    getBulletinData: boolean;
    getStudiesData: boolean;
    getCimData: boolean;
    getPositionsData: boolean;
    getRanksData: boolean;
    getFamilyComponentData: boolean;
    getTimeSheetTableData: boolean;

    getDocumentsDataIdentity: boolean;
    getDocumentsDataOrders: boolean;
    getDocumentsDataCim: boolean;
    getDocumentsDataRequest: boolean;
    getDocumentsDataVacation: boolean;
}
