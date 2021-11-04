using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;
using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Data.Entities.NomenclatureType
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
