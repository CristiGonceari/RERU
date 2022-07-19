﻿using CVU.ERP.Common.Data.Entities;
using CVU.ERP.StorageService.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities.Documents
{
    public class DocumentTemplateKey : SoftDeleteBaseEntity
    {
        public string KeyName { get; set; }
        public string Description { get; set; }
        public int? TranslateId { get; set; }

        public FileTypeEnum FileType { get; set; }
    }
}
