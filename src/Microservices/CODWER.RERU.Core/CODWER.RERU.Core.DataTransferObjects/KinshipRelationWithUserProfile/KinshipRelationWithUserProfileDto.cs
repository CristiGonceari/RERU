using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.KinshipRelationWithUserProfile
{
    public class KinshipRelationWithUserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Subdivision { get; set; }

        public int UserProfileId { get; set; }
    }
}
