using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionBulkTemplate
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_INTREBARI)]

    public class GetQuestionBulkTemplateQuery : IRequest<byte[]>
    {
        public QuestionTypeEnum QuestionType { get; set; }
    }
}
