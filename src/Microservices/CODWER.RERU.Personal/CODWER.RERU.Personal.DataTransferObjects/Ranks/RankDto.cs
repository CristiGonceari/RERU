using System;

namespace CODWER.RERU.Personal.DataTransferObjects.Ranks
{
    public class RankDto
    {
        public int Id { get; set; }
        public string Mentions { get; set; }
        public DateTime From { get; set; }

        public int RankRecordId { get; set; }
        public string RankRecordName { get; set; }
    }
}
