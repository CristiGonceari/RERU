using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.TestTypes.GetTestTypes
{
    public class GetTestTypesQueryHandler : IRequestHandler<GetTestTypesQuery, PaginatedModel<TestTypeDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetTestTypesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<TestTypeDto>> Handle(GetTestTypesQuery request, CancellationToken cancellationToken)
        {
            var testTypes = _appDbContext.TestTypes
                .Include(x => x.TestTypeQuestionCategories)
                .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTypes)
                .ThenInclude(x => x.Event)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                testTypes = testTypes.Where(x => x.Name.Contains(request.EventName));
            }

            if (request.Status.HasValue)
            {
                testTypes = testTypes.Where(x => x.Status == request.Status);
            }

            if (!string.IsNullOrEmpty(request.EventName))
            {
                testTypes = testTypes.Where(x => x.EventTestTypes.Any(x => x.Event.Name.Contains(request.EventName)));
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<TestType, TestTypeDto>(testTypes, request);

            return paginatedModel;
        }
    }
}
