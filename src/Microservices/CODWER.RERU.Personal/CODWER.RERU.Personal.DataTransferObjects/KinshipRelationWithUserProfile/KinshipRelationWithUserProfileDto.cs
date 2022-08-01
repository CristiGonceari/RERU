using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Personal.DataTransferObjects.KinshipRelationWithUserProfile
{
    public class KinshipRelationWithUserProfileDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Subdivision { get; set; }

        public int ContractorId { get; set; }
    }
}
