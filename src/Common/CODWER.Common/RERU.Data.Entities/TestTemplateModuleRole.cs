using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class TestTemplateModuleRole : SoftDeleteBaseEntity
    {
        public int TestTemplateId { get; set; }
        public TestTemplate TestTemplate { get; set; }

        public int ModuleRoleId { set; get; }
        public ModuleRole ModuleRole { set; get; }
    }
}
