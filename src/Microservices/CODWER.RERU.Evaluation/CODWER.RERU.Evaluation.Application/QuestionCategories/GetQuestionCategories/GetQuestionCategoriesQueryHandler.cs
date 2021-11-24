using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategories
{
    public class GetQuestionCategoriesQueryHandler : IRequestHandler<GetQuestionCategoriesQuery, PaginatedModel<QuestionCategoryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IPaginationService _paginationService;

        public GetQuestionCategoriesQueryHandler(AppDbContext appDbContext, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<QuestionCategoryDto>> Handle(GetQuestionCategoriesQuery request, CancellationToken cancellationToken)
        {
            var categories = _appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (request != null && !string.IsNullOrEmpty(request.Name))
            {
                categories = categories.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%"));
            }

            var paginatedModel = _paginationService.MapAndPaginateModel<Evaluation.Data.Entities.QuestionCategory, QuestionCategoryDto>(categories, request);

            return paginatedModel;
        }
    }
}
