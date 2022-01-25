using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Ranks
{
    public class AddEditRankDto
    {
        public int Id { get; set; }
        public string Mentions { get; set; }
        public DateTime From { get; set; }

        public int RankRecordId { get; set; } // nomenclature
        public int ContractorId { get; set; }
    }
}
