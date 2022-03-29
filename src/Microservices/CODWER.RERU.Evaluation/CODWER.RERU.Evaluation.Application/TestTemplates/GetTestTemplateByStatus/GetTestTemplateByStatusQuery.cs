using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus
{
    public class GetTestTemplateByStatusQuery : IRequest<List<SelectTestTemplateValueDto>>
    {
        public TestTemplateStatusEnum TestTemplateStatus { get; set; }
        public int? EventId { get; set; }
    }
}
