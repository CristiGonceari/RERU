using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.EventTestTemplates.GetNoAssignedTestTypes
{
    [ModuleOperation(permission: PermissionCodes.EVENTS_GENERAL_ACCESS)]

    public class GetNoAssignedTestTemplatesQuery : IRequest<List<TestTemplateDto>>
    {
        public int EventId { get; set; }
        public string Keyword { get; set; }
    }
}
