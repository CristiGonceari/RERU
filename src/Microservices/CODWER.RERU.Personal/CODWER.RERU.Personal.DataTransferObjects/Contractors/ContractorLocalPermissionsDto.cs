﻿namespace CODWER.RERU.Personal.DataTransferObjects.Contractors
{
    public class ContractorLocalPermissionsDto
    {
        public int ContractorId { get; set; }

        public bool GetGeneralData { get; set; }
        public bool GetBulletinData { get; set; }
        public bool GetStudiesData { get; set; }
        public bool GetCimData { get; set; }
        public bool GetPositionsData { get; set; }
        public bool GetRanksData { get; set; }
        public bool GetFamilyComponentData { get; set; }
        public bool GetTimeSheetTableData { get; set; }

        public bool GetDocumentsDataIdentity { get; set; }
        public bool GetDocumentsDataOrders { get; set; }
        public bool GetDocumentsDataCim { get; set; }
        public bool GetDocumentsDataRequest { get; set; }
        public bool GetDocumentsDataVacation { get; set; }
    }
}