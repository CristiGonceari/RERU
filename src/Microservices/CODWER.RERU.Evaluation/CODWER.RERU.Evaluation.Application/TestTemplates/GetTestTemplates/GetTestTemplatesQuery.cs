using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTemplates;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;
using RERU.Data.Entities.Enums;

namespace CODWER.RERU.Evaluation.Application.TestTemplates.GetTestTemplates
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_SABLOANELE_DE_TESTE)]

    public class GetTestTemplatesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<TestTemplateDto>>
    {
        public string Name { get; set; }
        public string EventName { get; set; }
        public QualifyingTypeEnum? QualifyingType { get; set; }
        public TestTemplateStatusEnum? Status { get; set; }
        public TestTemplateModeEnum? Mode { get; set; }
    }
}
