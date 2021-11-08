using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnit
{
    public class GetQuestionUnitQueryHandler : IRequestHandler<GetQuestionUnitQuery, QuestionUnitDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;

        public GetQuestionUnitQueryHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
        }

        public async Task<QuestionUnitDto> Handle(GetQuestionUnitQuery request, CancellationToken cancellationToken)
        {
            var questionUnit = await _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(x => x.QuestionUnitTags)
                    .ThenInclude(x => x.Tag)
                .FirstOrDefaultAsync(x => x.Id == request.Id);

            if (questionUnit != null)
            {
                if (questionUnit.QuestionType == QuestionTypeEnum.HashedAnswer)
                {
                    questionUnit = await _questionUnitService.GetUnHashedQuestionUnit(questionUnit.Id);
                    questionUnit.Options = null;
                }
            }

            var mappedData = _mapper.Map<QuestionUnitDto>(questionUnit);

            return mappedData;
        }
    }
}
