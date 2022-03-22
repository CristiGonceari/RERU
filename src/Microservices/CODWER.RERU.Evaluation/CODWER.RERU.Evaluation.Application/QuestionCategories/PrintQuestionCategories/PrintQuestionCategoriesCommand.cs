using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.Attributes;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.PrintQuestionCategories
{
    [ModuleOperation(permission: Permissions.PermissionCodes.QUESTION_CATEGORIES_GENERAL_ACCESS)]

    public class PrintQuestionCategoriesCommand : TableParameter, IRequest<FileDataDto>
    {
        public string Name { get; set; }
    }
}
