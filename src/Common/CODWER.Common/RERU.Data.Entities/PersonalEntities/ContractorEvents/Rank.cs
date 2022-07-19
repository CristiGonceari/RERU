using System;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.PersonalEntities.NomenclatureType.NomenclatureRecords;

namespace RERU.Data.Entities.PersonalEntities.ContractorEvents
{
    public class Rank : SoftDeleteBaseEntity
    {
        public string Mentions { get; set; }
        public DateTime From { get; set; }

        public int RankRecordId { get; set; }
        public NomenclatureRecord RankRecord { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
