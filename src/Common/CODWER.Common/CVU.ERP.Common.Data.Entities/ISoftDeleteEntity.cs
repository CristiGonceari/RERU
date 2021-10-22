using System;

namespace CVU.ERP.Common.Data.Entities
{
    public interface ISoftDeleteEntity
    {
        bool IsDeleted { set; get; }
        DateTime? DeleteTime { get; set; }
    }
}
