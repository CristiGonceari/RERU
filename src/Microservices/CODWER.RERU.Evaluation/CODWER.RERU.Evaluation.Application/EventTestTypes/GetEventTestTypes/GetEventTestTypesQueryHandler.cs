using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.EventTestTypes.GetEventTestTypes
{
    public class GetEventTestTypesQueryHandler : IRequestHandler<GetEventTestTypesQuery, PaginatedModel<TestTemplateDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventTestTypesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<TestTemplateDto>> Handle(GetEventTestTypesQuery request, CancellationToken cancellationToken)
        {
            var eventTestTypes = _appDbContext.EventTestTypes
                .Include(x => x.TestType)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.TestType)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<TestTemplate, TestTemplateDto>(eventTestTypes, request);
        }
    }
}
