using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategory
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_CATEGORII)]
    public class GetQuestionCategoryQuery : IRequest<QuestionCategoryDto>
    {
        public int Id { get; set; }
    }
}
