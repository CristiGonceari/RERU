using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;

namespace RERU.Data.Entities
{
    public class ArticleEvaluation : SoftDeleteBaseEntity
    {
        public ArticleEvaluation()
        {
            ArticleRoles = new HashSet<ArticleEvaluationModuleRole>();
        }

        public string Name { get; set; }
        public string Content { get; set; }
        public string MediaFileId { get; set; }

        public virtual ICollection<ArticleEvaluationModuleRole> ArticleRoles { set; get; }
    }
}
