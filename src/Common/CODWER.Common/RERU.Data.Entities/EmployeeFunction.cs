using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class EmployeeFunction : SoftDeleteBaseEntity
    {
        public string Name { set; get; }
        public EvaluationTypeEnum Type { set; get; }
        public int? ColaboratorId { set; get; }
    }
}
