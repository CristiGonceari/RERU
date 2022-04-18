using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategoriesNonPaginated
{
    public class GetQuestionCategoriesNonPaginatedQueryHandler : IRequestHandler<GetQuestionCategoriesNonPaginatedQuery, List<QuestionCategoryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetQuestionCategoriesNonPaginatedQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<List<QuestionCategoryDto>> Handle(GetQuestionCategoriesNonPaginatedQuery request, CancellationToken cancellationToken)
        {
            var categories = _appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                .OrderBy(x => x.Name)
                .AsQueryable();

            if (request != null && !string.IsNullOrEmpty(request.Name))
            {
                categories = categories.Where(x => EF.Functions.Like(x.Name, $"%{request.Name}%"));
            }

            return _mapper.Map<List<QuestionCategoryDto>>(await categories.ToListAsync());
        }
    }
}
