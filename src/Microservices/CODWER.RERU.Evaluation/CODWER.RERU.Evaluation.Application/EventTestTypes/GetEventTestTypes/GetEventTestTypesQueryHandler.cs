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
    public class GetEventTestTypesQueryHandler : IRequestHandler<GetEventTestTypesQuery, PaginatedModel<TestTypeDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetEventTestTypesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<TestTypeDto>> Handle(GetEventTestTypesQuery request, CancellationToken cancellationToken)
        {
            var eventTestTypes = _appDbContext.EventTestTypes
                .Include(x => x.TestType)
                .Where(x => x.EventId == request.EventId)
                .Select(x => x.TestType)
                .AsQueryable();

            return await _paginationService.MapAndPaginateModelAsync<TestType, TestTypeDto>(eventTestTypes, request);
        }
    }
}
