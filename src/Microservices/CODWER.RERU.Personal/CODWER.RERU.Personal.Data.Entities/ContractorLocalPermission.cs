using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities
{
    public class ContractorLocalPermission : SoftDeleteBaseEntity
    {
        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }

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
