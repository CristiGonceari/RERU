using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;

namespace RERU.Data.Entities.PersonalEntities.NomenclatureType
{
    public class NomenclatureType : SoftDeleteBaseEntity
    {
        public NomenclatureType()
        {
            NomenclatureColumns = new HashSet<NomenclatureColumn>();
            NomenclatureRecords = new HashSet<NomenclatureRecord>();
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }
        public BaseNomenclatureTypesEnum? BaseNomenclature { get; set; }

        public virtual ICollection<NomenclatureColumn> NomenclatureColumns { get; set; }
        public virtual ICollection<NomenclatureRecord> NomenclatureRecords { get; set; }
    }
}
