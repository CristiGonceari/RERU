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
        private readonly IStorageFileService _storageFileService;

        public EditQuestionUnitCommandHandler(AppDbContext appDbContext, IQuestionUnitService questionUnitService, IMediator mediator, IMapper mapper, IStorageFileService storageFileService)
        {
            _appDbContext = appDbContext;
            _questionUnitService = questionUnitService;
            _mediator = mediator;
            _mapper = mapper;
            _storageFileService = storageFileService;
        }

        public async Task<Unit> Handle(EditQuestionUnitCommand request, CancellationToken cancellationToken)
        {
            var editQuestionUnit = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(x => x.Id == request.Data.Id);

            if (editQuestionUnit.QuestionType != request.Data.QuestionType)
            {
                var deleteOptions = await _appDbContext.Options
                .Where(x => x.QuestionUnitId == editQuestionUnit.Id)
                .ToListAsync();

                _appDbContext.Options.RemoveRange(deleteOptions);
            }

            if (request.Data.FileDto != null)
            {

                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                editQuestionUnit.MediaFileId = addFile;
            }
            else
            {
                editQuestionUnit.MediaFileId = null;
            }

            _mapper.Map(request.Data, editQuestionUnit);

            await _appDbContext.SaveChangesAsync();

            if (editQuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                await _questionUnitService.HashQuestionUnit(editQuestionUnit.Id);
            }

            await _mediator.Send(new AssignTagToQuestionUnitCommand { QuestionUnitId = editQuestionUnit.Id, Tags = request.Data.Tags });

            return Unit.Value;
        }
    }
}
