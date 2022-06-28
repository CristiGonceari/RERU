using System.Collections.Generic;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplateByStatus
{
    public class GetTestTemplateByStatusQuery : IRequest<List<SelectTestTemplateValueDto>>
    {
        public TestTemplateStatusEnum TestTemplateStatus { get; set; }
        public int? EventId { get; set; }
        public TestTemplateModeEnum Mode { get; set; }
    }
}
