using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.Enums;

namespace RERU.Data.Entities.PersonalEntities.NomenclatureType
{
    public class NomenclatureColumn : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public FieldTypeEnum Type { get; set; }
        public bool IsMandatory { get; set; }
        public int Order { get; set; }

        public int NomenclatureTypeId { get; set; }
        public NomenclatureType NomenclatureType { get; set; }
    }
}
