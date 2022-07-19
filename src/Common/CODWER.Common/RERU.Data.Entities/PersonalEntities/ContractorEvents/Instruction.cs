using System;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.ContractorEvents
{
    public class Instruction : SoftDeleteBaseEntity
    {
        public string Thematic { get; set; }
        public string InstructorName { get; set; }
        public string InstructorLastName { get; set; }
        public int Duration { get; set; }
        public DateTime Date { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
