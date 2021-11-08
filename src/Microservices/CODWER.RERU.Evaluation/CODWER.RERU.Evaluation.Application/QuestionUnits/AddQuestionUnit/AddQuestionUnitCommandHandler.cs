using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit
{
    public class AddQuestionUnitCommandHandler : IRequestHandler<AddQuestionUnitCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IMediator _mediator;

        public AddQuestionUnitCommandHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService, IMediator mediator)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
            _mediator = mediator;
        }

        public async Task<int> Handle(AddQuestionUnitCommand request, CancellationToken cancellationToken)
        {
            var newQuestionUnit = _mapper.Map<QuestionUnit>(request.Data);

            if (newQuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                await _questionUnitService.HashQuestionUnit(newQuestionUnit.Id);
            }

            await _appDbContext.QuestionUnits.AddAsync(newQuestionUnit);

            await _appDbContext.SaveChangesAsync();

            await _mediator.Send(new AssignTagToQuestionUnitCommand { QuestionUnitId = newQuestionUnit.Id, Tags = request.Data.Tags });

            return newQuestionUnit.Id;
        }
    }
}
