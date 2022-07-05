using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using System;

namespace RERU.Data.Entities
{
    public class KinshipRelation : SoftDeleteBaseEntity
    {
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthLocation { get; set; }
        public string Function { get; set; }
        public string WorkLocation { get; set; }
        public string ResidenceAddress { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
