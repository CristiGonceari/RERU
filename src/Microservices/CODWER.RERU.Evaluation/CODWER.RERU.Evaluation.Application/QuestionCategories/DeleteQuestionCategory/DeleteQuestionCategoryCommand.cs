using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.DeleteQuestionCategory
{
    [ModuleOperation(permission: Permissions.PermissionCodes.ACCES_GENERAL_LA_CATEGORII)]
    public class DeleteQuestionCategoryCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
