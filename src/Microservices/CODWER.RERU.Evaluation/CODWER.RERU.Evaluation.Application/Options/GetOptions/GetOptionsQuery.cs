using CVU.ERP.Module.Application.Attributes;
using MediatR;
using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.Options;

namespace CODWER.RERU.Evaluation.Application.Options.GetOptions
{
    [ModuleOperation(permission: PermissionCodes.OPTIONS_GENERAL_ACCESS)]
    public class GetOptionsQuery : IRequest<List<OptionDto>>
    {
        public int? QuestionUnitId { get; set; }
    }
}
