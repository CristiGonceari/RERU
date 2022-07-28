namespace CODWER.RERU.Core.DataTransferObjects.RecommendationForStudy
{
    public class RecommendationForStudyDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public string Subdivision { get; set; }
        public int UserProfileId { get; set; }
    }
}
