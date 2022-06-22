using CVU.ERP.Common.Data.Entities;
using System;

namespace RERU.Data.Entities
{
    public class MilitaryBooklet : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Series { get; set; }
        public int Number { get; set; }
        public DateTime ReleaseDay { get; set; }
        public string EminentAuthority { get; set; }

        public int MilitaryObligationId { get; set; }
        public MilitaryObligation MilitaryObligation { get; set; }
    }
}
