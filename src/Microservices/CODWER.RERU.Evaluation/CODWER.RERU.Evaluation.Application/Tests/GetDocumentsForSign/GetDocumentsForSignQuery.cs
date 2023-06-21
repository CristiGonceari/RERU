using System.Collections.Generic;
using CODWER.RERU.Evaluation.Application.Permissions;
using CODWER.RERU.Evaluation.DataTransferObjects.DocumentForSign;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.GetDocumentsForSign
{
    [ModuleOperation(permission: PermissionCodes.ACCES_GENERAL_LA_TESTE)]
    public class GetDocumentsForSignQuery : IRequest<List<DocumentForSignDto>>
    {
        public int? TestId { get; set; }
    }
}
