using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleEvaluationModuleRole : BaseEntity
    {
        public int RoleId { set; get; }
        public ModuleRole Role { set; get; }

        public int ArticleId { set; get; }
        public ArticleEvaluation Article { set; get; }
    }
}
