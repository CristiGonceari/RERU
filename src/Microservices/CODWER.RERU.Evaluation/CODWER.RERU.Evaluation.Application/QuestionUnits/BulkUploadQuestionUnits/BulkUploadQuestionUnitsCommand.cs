using CODWER.RERU.Evaluation.Application.Permissions;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using Microsoft.AspNetCore.Http;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.BulkUploadQuestionUnits
{
    [ModuleOperation(permission: PermissionCodes.QUESTION_UNITS_GENERAL_ACCESS)]
    public class BulkUploadQuestionUnitsCommand : IRequest<byte[]>
    {
        public IFormFile Input { get; set; }
    }
}
