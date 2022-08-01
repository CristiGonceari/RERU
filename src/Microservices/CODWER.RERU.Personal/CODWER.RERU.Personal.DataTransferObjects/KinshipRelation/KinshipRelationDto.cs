using RERU.Data.Entities.Enums;
using System;

namespace CODWER.RERU.Personal.DataTransferObjects.KinshipRelation
{
    public class KinshipRelationDto
    {
        public int Id { get; set; }
        public KinshipDegreeEnum KinshipDegree { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public DateTime BirthDate { get; set; }
        public string BirthLocation { get; set; }
        public string Function { get; set; }
        public string WorkLocation { get; set; }
        public string ResidenceAddress { get; set; }
        public int ContractorId { get; set; }

    }
}
