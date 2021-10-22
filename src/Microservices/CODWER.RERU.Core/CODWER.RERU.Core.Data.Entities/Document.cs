using CVU.ERP.Common.Data.Entities;
using System;

namespace CODWER.RERU.Core.Data.Entities
{
    public class Document : BaseEntity
    {
        public string Name { get; set; }
        public string Type { get; set; }
        public Guid DocumentStorageId { get; set; }
    }
}
