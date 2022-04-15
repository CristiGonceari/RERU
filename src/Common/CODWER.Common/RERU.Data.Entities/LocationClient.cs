using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class LocationClient : SoftDeleteBaseEntity
    {
        public int Number { get; set; }
        public string Note { get; set; }
        public string Token { get; set; }

        public int LocationId { get; set; }
        public Location Location { get; set; }
    }
}
