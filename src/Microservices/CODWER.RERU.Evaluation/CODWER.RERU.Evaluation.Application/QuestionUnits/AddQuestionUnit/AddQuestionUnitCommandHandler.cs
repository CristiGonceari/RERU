using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.QuestionUnits.AssignTagToQuestionUnit;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.AddQuestionUnit
{
    public class AddQuestionUnitCommandHandler : IRequestHandler<AddQuestionUnitCommand, int>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IMediator _mediator;
        private readonly IStorageFileService _storageFileService;

        public AddQuestionUnitCommandHandler(
                        AppDbContext appDbContext, 
                        IMapper mapper, 
                        IQuestionUnitService questionUnitService, 
                        IMediator mediator,
                        IStorageFileService iStorageFileService
            )
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
            _mediator = mediator;
            _storageFileService = iStorageFileService;
        }

        public async Task<int> Handle(AddQuestionUnitCommand request, CancellationToken cancellationToken)
        {

            var storage = await _storageFileService.AddFile(request.FileDto);


            var newQuestion = new AddEditQuestionUnitDto()
            {
                QuestionCategoryId = request.QuestionCategoryId,
                Question = request.Question,
                Tags = request.Tags,
                QuestionType = request.QuestionType,
                Status = request.Status,
                QuestionPoints = request.QuestionPoints,
                MediaFileId = storage,
             };

            var newQuestionUnit =  _mapper.Map<QuestionUnit>(newQuestion);

            await _appDbContext.QuestionUnits.AddAsync(newQuestionUnit);

            await _appDbContext.SaveChangesAsync();

            if (newQuestionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
            {
                await _questionUnitService.HashQuestionUnit(newQuestionUnit.Id);
            }

            await _mediator.Send(new AssignTagToQuestionUnitCommand { QuestionUnitId = newQuestionUnit.Id, Tags = request.Tags });

            return newQuestionUnit.Id;
        }
    }
}
