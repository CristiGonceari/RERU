using CVU.ERP.OrganigramService.Models;
using CVU.ERP.OrganigramService.Services;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.Organigrams.GetOrganigramContent
{
    public class GetOrganigramContentQueryHandler : IRequestHandler<GetOrganigramContentQuery, List<OrganigramContent>>
    {
        private readonly IGetOrganigramService _service;

        public GetOrganigramContentQueryHandler(IGetOrganigramService service)
        {
            _service = service;
        }

        public Task<List<OrganigramContent>> Handle(GetOrganigramContentQuery request, CancellationToken cancellationToken)
        {
            return _service.GetOrganigramContent(request.ParentDepartmentId, request.Type, request.OrganigramId);
        }
    }
}
