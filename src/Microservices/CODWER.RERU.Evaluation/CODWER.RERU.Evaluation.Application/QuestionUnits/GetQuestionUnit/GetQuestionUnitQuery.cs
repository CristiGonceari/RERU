using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]

    public class GetQuestionUnitQuery : IRequest<QuestionUnitDto>
    {
        public int Id { get; set; }
    }
}
