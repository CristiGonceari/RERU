using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords.RecordValues
{
    public abstract class RecordValue : SoftDeleteBaseEntity
    {
        public int NomenclatureRecordId { get; set; }
        public NomenclatureRecord NomenclatureRecord { get; set; }

        public int NomenclatureColumnId { get; set; }
        public NomenclatureColumn NomenclatureColumn { get; set; }
    }
}
