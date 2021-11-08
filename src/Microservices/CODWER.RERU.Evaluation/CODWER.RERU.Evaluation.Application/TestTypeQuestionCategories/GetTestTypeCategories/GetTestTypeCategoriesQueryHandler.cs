using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypeQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.GetTestTypeCategories
{
    public class GetTestTypeCategoriesQueryHandler : IRequestHandler<GetTestTypeCategoriesQuery, List<TestTypeQuestionCategoryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTypeCategoriesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<TestTypeQuestionCategoryDto>> Handle(GetTestTypeCategoriesQuery request, CancellationToken cancellationToken)
        {
            var questionCategories = await _appDbContext.TestTypeQuestionCategories
                .Include(x => x.QuestionCategory)
                .Where(x => x.TestTypeId == request.TestTypeId)
                .ToListAsync();

            var answer = _mapper.Map<List<TestTypeQuestionCategoryDto>>(questionCategories);
            var testType = await _appDbContext.TestTypes.FirstAsync(x => x.Id == request.TestTypeId);

            if (testType.CategoriesSequence == SequenceEnum.Strict)
            {
                answer = answer.OrderBy(x => x.CategoryIndex).ToList();
            }
            else
            {
                answer = answer.OrderBy(x => Guid.NewGuid()).ToList();
            }

            return answer;
        }
    }
}
