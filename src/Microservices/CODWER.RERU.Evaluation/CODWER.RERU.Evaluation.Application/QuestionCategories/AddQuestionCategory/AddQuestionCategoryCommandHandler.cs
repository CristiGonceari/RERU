using AutoMapper;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.AddQuestionCategory
{
    public class AddQuestionCategoryCommandHandler: IRequestHandler<AddQuestionCategoryCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;

        public AddQuestionCategoryCommandHandler(AppDbContext appDbContext, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
        }

        public async Task<int> Handle(AddQuestionCategoryCommand request, CancellationToken cancellationToken)
        {
            var questionCategoryToCreate = _mapper.Map<Evaluation.Data.Entities.QuestionCategory>(request.Data);

            await _appDbContext.QuestionCategories.AddAsync(questionCategoryToCreate);
            await _appDbContext.SaveChangesAsync();

            return questionCategoryToCreate.Id;
        }
    }
}
