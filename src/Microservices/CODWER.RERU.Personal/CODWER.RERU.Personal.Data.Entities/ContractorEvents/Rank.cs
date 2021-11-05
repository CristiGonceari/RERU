using System;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;
using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.ContractorEvents
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
