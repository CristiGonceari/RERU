using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues
{
    public class RecordValueDto
    {
        public int Id { get; set; }

        public int NomenclatureRecordId { get; set; }
        public int NomenclatureColumnId { get; set; }

        public string StringValue { get; set; }
        public FieldTypeEnum Type { get; set; }
    }
}
