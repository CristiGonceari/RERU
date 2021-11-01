using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords.RecordValues;
using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;

namespace CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords
{
    public class NomenclatureRecord : SoftDeleteBaseEntity
    {
        public NomenclatureRecord()
        {
            RecordValues = new HashSet<RecordValue>();
        }

        public string Name { get; set; }
        public bool IsActive { get; set; }

        public int NomenclatureTypeId { get; set; }
        public NomenclatureType NomenclatureType { get; set; }
        public virtual ICollection<RecordValue> RecordValues { get; set; }
    }
}
