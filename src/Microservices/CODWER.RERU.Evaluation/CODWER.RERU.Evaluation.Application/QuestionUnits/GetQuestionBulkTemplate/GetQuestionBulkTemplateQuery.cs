using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionBulkTemplate
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class GetQuestionBulkTemplateQuery : IRequest<byte[]>
    {
        public QuestionTypeEnum QuestionType { get; set; }
    }
}
