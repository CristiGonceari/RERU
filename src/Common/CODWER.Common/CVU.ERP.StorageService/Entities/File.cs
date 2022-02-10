using System;
using CVU.ERP.Common.Data.Entities;

namespace CVU.ERP.StorageService.Entities
{
    public class File : SoftDeleteBaseEntity
    {
        public Guid Id { get; set; }
        public string FileName { get; set; }
        public string UniqueFileName { get; set; }
        public string Type { get; set; }
        public FileTypeEnum FileType { get; set; }
        public string BucketName { get; set; }
    }
}
