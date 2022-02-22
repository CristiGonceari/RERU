using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetQuestionsPreview
{
    [ModuleOperation(permission: PermissionCodes.TEST_TEMPLATES_GENERAL_ACCESS)]

    public class GetQuestionsPreviewQuery : IRequest<List<QuestionUnitPreviewDto>>
    {
        public int TestTemplateId { get; set; }
    }
}
