using CODWER.RERU.Personal.Data.Entities.Enums;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.NomenclatureType
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
