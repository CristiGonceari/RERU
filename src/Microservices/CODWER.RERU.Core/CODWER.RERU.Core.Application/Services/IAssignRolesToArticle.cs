using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Core.DataTransferObjects;

namespace CODWER.RERU.Core.Application.Services
{
    public interface IAssignRolesToArticle
    {
        Task AssignRolesToArticle(List<AssignTagsValuesDto> requiredDocuments, int articleId);
    }
}
