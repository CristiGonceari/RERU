using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords
{
    public class RecordToValidate
    {
        public int NomenclatureRecordId { get; set; }
        public BaseNomenclatureTypesEnum BaseNomenclatureTypes { get; set; }
    }
}
