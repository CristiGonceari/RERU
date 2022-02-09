using AutoMapper;
using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.TestTypeQuestionCategories.AssignQuestionCategoryToTestTemplate
{
    public class AssignQuestionCategoryToTestTemplateCommandHandler : IRequestHandler<AssignQuestionCategoryToTestTemplateCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AssignQuestionCategoryToTestTemplateCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AssignQuestionCategoryToTestTemplateCommand request, CancellationToken cancellationToken)
        {
            var newQuestionCategoryTestType = _mapper.Map<TestTypeQuestionCategory>(request.Data);

            await _appDbContext.TestTypeQuestionCategories.AddAsync(newQuestionCategoryTestType);
            await _appDbContext.SaveChangesAsync();

            if (request.Data.SelectionType == SelectionEnum.Select)
            {
                var itemsList = request.Data.TestCategoryQuestions;
                foreach (var item in itemsList)
                {
                    item.TestTypeQuestionCategoryId = newQuestionCategoryTestType.Id;
                }

                var itemsListToAdd = _mapper.Map<List<TestCategoryQuestion>>(itemsList);

                await _appDbContext.TestCategoryQuestions.AddRangeAsync(itemsListToAdd);
                await _appDbContext.SaveChangesAsync();
            }

            return newQuestionCategoryTestType.Id;
        }
    }
}
