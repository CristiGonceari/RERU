using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class ModernLanguageLevel : SoftDeleteBaseEntity
    {
        public int ModernLanguageId { get; set; }
        public ModernLanguage ModernLanguage { get; set; }

        public KnowledgeQuelifiersEnum KnowledgeQuelifiers { get; set; }

        public int UserProfileId { get; set; }
        public UserProfile UserProfile { get; set; }
    }
}
