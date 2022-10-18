using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects;

namespace CODWER.RERU.Evaluation.Application.Services
{
    public interface IAssignRolesToArticle
    {
        Task AssignRolesToArticle(List<AssignTagsValuesDto> requiredDocuments, int articleId);
    }
}
