using CVU.ERP.Common.Data.Entities;

namespace CODWER.RERU.Personal.Data.Entities.ContractorEvents
{
    public class Badge : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }

        public int ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
