﻿using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class KinshipRelationWithUserProfile : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string LastName { get; set; }
        public string Function { get; set; }
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Subdivision { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
