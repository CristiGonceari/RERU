﻿using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Core.DataTransferObjects.ModernLanguageLevel
{
    public class ModernLanguageLevelDto
    {
        public int ModernLanguageId { get; set; }
        public KnowledgeQuelifiersEnum KnowledgeQuelifiers { get; set; }
        public int ContractorId { get; set; }

    }
}
