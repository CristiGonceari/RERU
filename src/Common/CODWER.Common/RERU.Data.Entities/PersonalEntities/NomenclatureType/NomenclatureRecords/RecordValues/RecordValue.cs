using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues
{
    public abstract class RecordValue : SoftDeleteBaseEntity
    {
        public int NomenclatureRecordId { get; set; }
        public NomenclatureRecord NomenclatureRecord { get; set; }

        public int NomenclatureColumnId { get; set; }
        public NomenclatureColumn NomenclatureColumn { get; set; }
    }
}
