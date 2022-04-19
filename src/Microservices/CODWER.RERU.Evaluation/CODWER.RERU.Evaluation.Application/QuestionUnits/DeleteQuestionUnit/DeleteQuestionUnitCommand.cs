using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.DeleteQuestionUnit
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_ÎNTREBĂRI)]

    public class DeleteQuestionUnitCommand : IRequest<Unit>
    {
        public int Id { get; set; }
    }
}
