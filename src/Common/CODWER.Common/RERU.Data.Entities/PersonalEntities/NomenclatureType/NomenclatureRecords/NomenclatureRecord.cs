using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords.RecordValues;

namespace RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords
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
