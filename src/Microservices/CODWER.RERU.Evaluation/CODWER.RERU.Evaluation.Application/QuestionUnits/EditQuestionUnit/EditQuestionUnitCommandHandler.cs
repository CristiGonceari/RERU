using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit
{
    public class EditQuestionUnitCommandHandler : IRequestHandler<EditQuestionUnitCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        public EditQuestionUnitCommandHandler(AppDbContext appDbContext, IQuestionUnitService questionUnitService, IMediator mediator, IMapper mapper)
        {
            _appDbContext = appDbContext;
            _questionUnitService = questionUnitService;
            _mediator = mediator;
            _mapper = mapper;
        }

        public async Task<Unit> Handle(EditQuestionUnitCommand request, CancellationToken cancellationToken)
        {
            var editQuestionUnit = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == request.Data.Id.Value);

            if (editQuestionUnit.QuestionType != request.Data.QuestionType)
            {
                var deleteOptions = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == editQuestionUnit.Id)
                .ToListAsync();

                _appDbContext.Options.RemoveRange(deleteOptions);
            }

            _mapper.Map(request.Data, editQuestionUnit);

            await _appDbContext.SaveChangesAsync();

            if (request.Data.Tags != null && request.Data.Tags.Count > 0)
            {
                await _mediator.Send(new AssignTagToQuestionUnitCommand { QuestionUnitId = editQuestionUnit.Id, Tags = request.Data.Tags });
            }

            if (editQuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                await _questionUnitService.HashQuestionUnit(editQuestionUnit.Id);
            }

            return Unit.Value;
        }
    }
}
