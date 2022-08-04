using RERU.Data.Entities.PersonalEntities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords
{
    public class RecordToValidate
    {
        public int NomenclatureRecordId { get; set; }
        public BaseNomenclatureTypesEnum BaseNomenclatureTypes { get; set; }
    }
}
