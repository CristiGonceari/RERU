using CVU.ERP.Common.Data.Entities;
using System.Collections.Generic;

namespace RERU.Data.Entities
{
    public class CandidatePosition : SoftDeleteBaseEntity
    {
        public string Name { get; set; }
        public bool IsActive { get; set; }
    }
}
