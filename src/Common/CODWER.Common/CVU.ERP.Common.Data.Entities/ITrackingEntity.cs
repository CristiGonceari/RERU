using System;
using System.Collections.Generic;
using System.Text;

namespace CVU.ERP.Common.Data.Entities
{
    public interface ITrackingEntity
    {
        int Id { get; set; }
        string CreateById { get; set; }
        string UpdateById { get; set; }
        DateTime CreateDate { get; set; }
        DateTime? UpdateDate { get; set; }
    }
}
