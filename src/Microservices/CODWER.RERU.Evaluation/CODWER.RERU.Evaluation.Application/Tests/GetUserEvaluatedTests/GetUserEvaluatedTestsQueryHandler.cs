using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests.GetUserEvaluatedTests
{
    public class GetUserEvaluatedTestsQueryHandler : IRequestHandler<GetUserEvaluatedTestsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;
        private readonly IMapper _mapper;

        public GetUserEvaluatedTestsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
            _mapper = mapper;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetUserEvaluatedTestsQuery request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestType)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => t.EvaluatorId == request.UserId || 
                            _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == request.UserId))
                .AsQueryable();
            
            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(userTests, request);

            return paginatedModel;
        }
    }
}
