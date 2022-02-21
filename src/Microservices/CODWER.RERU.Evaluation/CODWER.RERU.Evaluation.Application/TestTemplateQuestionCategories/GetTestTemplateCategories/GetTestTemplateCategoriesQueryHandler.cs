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
using CODWER.RERU.Evaluation.DataTransferObjects.testTemplateQuestionCategories;

namespace CODWER.RERU.Evaluation.Application.TestTemplateQuestionCategories.GetTestTemplateCategories
{
    public class GetTestTemplateCategoriesQueryHandler : IRequestHandler<GetTestTemplateCategoriesQuery, List<TestTemplateQuestionCategoryDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public GetTestTemplateCategoriesQueryHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<List<TestTemplateQuestionCategoryDto>> Handle(GetTestTemplateCategoriesQuery request, CancellationToken cancellationToken)
        {
            var questionCategories = await _appDbContext.testTemplateQuestionCategories
                .Include(x => x.QuestionCategory)
                .Where(x => x.TestTemplateId == request.TestTemplateId)
                .ToListAsync();

            var answer = _mapper.Map<List<TestTemplateQuestionCategoryDto>>(questionCategories);
            var testTemplate = await _appDbContext.TestTemplates.FirstAsync(x => x.Id == request.TestTemplateId);

            if (testTemplate.CategoriesSequence == SequenceEnum.Strict)
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
