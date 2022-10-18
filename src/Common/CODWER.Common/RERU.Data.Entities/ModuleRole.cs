using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities.PersonalEntities;

namespace RERU.Data.Entities
{
    public class ModuleRole : SoftDeleteBaseEntity {

        public ModuleRole()
        {
            UserProfileModuleRoles = new HashSet<UserProfileModuleRole>();
            ArticleEvaluationRoles = new HashSet<ArticleEvaluationModuleRole>();
            ArticleCoreRoles = new HashSet<ArticleCoreModuleRole>();
            TestTemplateModuleRoles = new HashSet<TestTemplateModuleRole>();
            ArticlePersonalRoles = new HashSet<ArticlePersonalModuleRole>();
        }

        public int ModuleId { set; get; }
        public string Name { set; get; }
        public string Code { set; get; }
        public bool IsAssignByDefault { set; get; }
        public RoleTypeEnum Type { set; get; }
        public Module Module { set; get; }

        public List<ModuleRolePermission> Permissions { set; get; }
        public virtual ICollection<ArticleEvaluationModuleRole> ArticleEvaluationRoles { set; get; }
        public virtual ICollection<ArticleCoreModuleRole> ArticleCoreRoles { set; get; }
        public virtual ICollection<ArticlePersonalModuleRole> ArticlePersonalRoles { set; get; }
        public virtual ICollection<UserProfileModuleRole> UserProfileModuleRoles { set; get; }
        public virtual ICollection<TestTemplateModuleRole> TestTemplateModuleRoles { set; get; }
    }
}