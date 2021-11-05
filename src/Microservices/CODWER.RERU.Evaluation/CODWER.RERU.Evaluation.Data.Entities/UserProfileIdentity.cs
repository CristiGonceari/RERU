namespace CODWER.RERU.Evaluation.Data.Entities
{
    public class UserProfileIdentity
    {
        public int Id { get; set; }
        public int UserProfileId { get; set; }
        public string Type { get; set; }
        public string Identificator { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
