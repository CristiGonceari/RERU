using System.Collections.Generic;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation360.DataTransferObjects;

namespace CODWER.RERU.Evaluation360.Application.BLL.Services
{
    public interface IAssignRolesToArticle
    {
        Task AssignRolesToArticle(List<AssignTagsValuesDto> requiredDocuments, int articleId);
    }
}
