using CODWER.RERU.Personal.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns
{
    public class NomenclatureColumnItemDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public FieldTypeEnum Type { get; set; }
        public bool IsMandatory { get; set; }
        public int Order { get; set; }
    }
}
