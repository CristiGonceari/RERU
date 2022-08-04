using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;

namespace RERU.Data.Entities
{
    public class ModernLanguageLevel : SoftDeleteBaseEntity
    {
        public int ModernLanguageId { get; set; }
        public ModernLanguage ModernLanguage { get; set; }

        public KnowledgeQuelifiersEnum KnowledgeQuelifiers { get; set; }

        public int? ContractorId { get; set; }
        public Contractor Contractor { get; set; }
    }
}
