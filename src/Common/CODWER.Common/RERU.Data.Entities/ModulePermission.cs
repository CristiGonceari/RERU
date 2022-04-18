using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ModulePermission : BaseEntity {
        public int ModuleId { set; get; }
        public string Code { set; get; }
        public string Description { set; get; }
    }
}