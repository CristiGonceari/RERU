using RERU.Data.Entities;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel
{
    public class ModernLanguageLevelDto
    {
        public ModernLanguage ModernLanguage { get; set; }
        public KnowledgeQuelifiersEnum KnowledgeQuelifiers { get; set; }
        public int UserProfileId { get; set; }

    }
}
