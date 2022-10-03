using AutoMapper;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit;
using CODWER.RERU.Evaluation.Application.Services;
using CVU.ERP.StorageService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.EditQuestionUnit
{
    public class EditQuestionUnitCommandHandler : IRequestHandler<EditQuestionUnitCommand, Unit>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;
        private readonly IStorageFileService _storageFileService;

        public EditQuestionUnitCommandHandler(AppDbContext appDbContext, 
            IQuestionUnitService questionUnitService, 
            IMediator mediator, IMapper mapper, 
            IStorageFileService storageFileService)
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

            _mapper.Map(request.Data, editQuestionUnit);

            if (request.Data.FileDto != null)
            {

                var addFile = await _storageFileService.AddFile(request.Data.FileDto);

                editQuestionUnit.MediaFileId = addFile;
            }
            else
            {
                editQuestionUnit.MediaFileId = request.Data.MediaFileId;
            }

            await _appDbContext.SaveChangesAsync();

            if (editQuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                await _questionUnitService.HashQuestionUnit(editQuestionUnit.Id);
            }

            if (request.Data.Tags != null)
            {
                await _mediator.Send(new AssignTagToQuestionUnitCommand { QuestionUnitId = editQuestionUnit.Id, Tags = request.Data.Tags });
            }

            return Unit.Value;
        }
    }
}
