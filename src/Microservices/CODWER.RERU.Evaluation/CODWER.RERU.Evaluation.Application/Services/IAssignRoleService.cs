using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IAssignRoleService
    {
        Task AssignRolesToArticle(List<AssignTagsValuesDto> requiredDocuments, int articleId);
        Task AssignRolesToTestTemplate(List<AssignTagsValuesDto> requiredDocuments, int testTemplateId);
    }
}
