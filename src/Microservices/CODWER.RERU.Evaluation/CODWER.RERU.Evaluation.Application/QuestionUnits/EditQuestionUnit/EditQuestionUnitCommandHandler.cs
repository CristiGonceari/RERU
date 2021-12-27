using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Files;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
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


            _mapper.Map(request, editQuestionUnit);

            await _appDbContext.SaveChangesAsync();

            if (editQuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                await _questionUnitService.HashQuestionUnit(editQuestionUnit.Id);
            }

            await _mediator.Send(new AssignTagToQuestionUnitCommand { QuestionUnitId = editQuestionUnit.Id, Tags = request.Data.Tags });

            if (request.Data.MediaFileId !="null")
            {
                var changeMediaFileId = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(qu => qu.MediaFileId == request.Data.MediaFileId);

                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                ChangeMediaFileId(changeMediaFileId, addFile);
            }
            else 
            {
                var changeMediaFileId = await _appDbContext.QuestionUnits.FirstOrDefaultAsync(qu => qu.Id == request.Data.Id);

                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                ChangeMediaFileId(changeMediaFileId, addFile);
            }

            return Unit.Value;
        }
        private async Task ChangeMediaFileId(QuestionUnit questionUnit, string addFile)
        {

            questionUnit.MediaFileId = addFile;

            //_mapper.Map<QuestionUnit>(questionUnit);

            await _appDbContext.SaveChangesAsync();
        }
    }
}
