using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetActiveQuestionUnitsValue
{ 
    public class GetActiveQuestionUnitsValueQueryHandler : IRequestHandler<GetActiveQuestionUnitsValueQuery, PaginatedModel<ActiveQuestionUnitValueDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IPaginationService _paginationService;

        public GetActiveQuestionUnitsValueQueryHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
            _paginationService = paginationService;
        }
        public async Task<PaginatedModel<ActiveQuestionUnitValueDto>> Handle(GetActiveQuestionUnitsValueQuery request, CancellationToken cancellationToken)
        {
            var questions = _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .OrderByDescending(x => x.CreateDate)
                .Where(x => x.Status == QuestionUnitStatusEnum.Active)
                .AsQueryable();

            if (request.Type != null)
            {
                questions = questions.Where(x => x.QuestionType == request.Type.Value);
            }

            if (request.CategoryId.HasValue)
            {
                questions = questions.Where(x => x.QuestionCategoryId == request.CategoryId);
            }

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<QuestionUnit, ActiveQuestionUnitValueDto>(questions, request);
            var items = paginatedModel.Items.ToList();

            var hashedQuestions = items.Where(x => x.QuestionType == QuestionTypeEnum.HashedAnswer).ToList();

            foreach (var unit in hashedQuestions)
            {
                items.Remove(unit);
                var unhashedQuestion = await _questionUnitService.GetUnHashedQuestionUnit(unit.Id);
                unhashedQuestion.Options = null;
                items.Add(_mapper.Map<ActiveQuestionUnitValueDto>(unhashedQuestion));
            }

            paginatedModel.Items = items;

            return paginatedModel;
        }
    }
}
