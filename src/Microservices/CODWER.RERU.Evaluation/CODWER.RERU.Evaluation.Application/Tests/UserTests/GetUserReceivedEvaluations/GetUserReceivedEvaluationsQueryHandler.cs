using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.GetUserReceivedEvaluations
{
    public class GetUserReceivedEvaluationsQueryHandler : IRequestHandler<GetUserReceivedEvaluationsQuery, PaginatedModel<TestDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetUserReceivedEvaluationsQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<TestDto>> Handle(GetUserReceivedEvaluationsQuery request, CancellationToken cancellationToken)
        {
            var evaluations = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Evaluator)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == request.UserId && t.TestTemplate.Mode == TestTemplateModeEnum.Evaluation)
                .OrderByDescending(x => x.ProgrammedTime)
                .AsQueryable();

            if (request.FromDate.HasValue)
            {
                evaluations = evaluations.Where(x => x.EndTime >= request.FromDate);
            }

            if (request.ToDate.HasValue)
            {
                evaluations = evaluations.Where(x => x.EndTime <= request.ToDate);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<Test, TestDto>(evaluations, request);

            return paginatedModel;
        }
    }
}
