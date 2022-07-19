using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities.PersonalEntities.ContractorEvents
{
    public class Bonus : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Value { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
