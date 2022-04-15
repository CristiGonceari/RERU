using AutoMapper;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.GetQuestionCategory
{
    public class GetQuestionCategoryQueryHandler : IRequestHandler<GetQuestionCategoryQuery, QuestionCategoryDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetQuestionCategoryQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }
        public async Task<QuestionCategoryDto> Handle(GetQuestionCategoryQuery request, CancellationToken cancellationToken)
        {
            var category = await _appDbContext.QuestionCategories
                .Include(x => x.QuestionUnits)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            return _mapper.Map<QuestionCategoryDto>(category);
        }
    }
}

