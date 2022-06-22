using CVU.ERP.Common.Data.Entities;


namespace RERU.Data.Entities
{
    public class RecommendationForStudy : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public string Subdivision { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
