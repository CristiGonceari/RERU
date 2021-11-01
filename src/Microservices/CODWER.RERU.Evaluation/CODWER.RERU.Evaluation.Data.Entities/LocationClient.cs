using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class LocationClient : SoftDeleteBaseEntity
    {
        public int LocationId { get; set; }
        public int Number { get; set; }
        public string Note { get; set; }
        public string Token { get; set; }
        public Location Location { get; set; }
    }
}
