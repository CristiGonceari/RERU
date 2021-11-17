using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.GetQuestionUnits
{
    public class GetQuestionUnitsQueryHandler : IRequestHandler<GetQuestionUnitsQuery, PaginatedModel<QuestionUnitDto>>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly IQuestionUnitService _questionUnitService;
        private readonly IPaginationService _paginationService;

        public GetQuestionUnitsQueryHandler(AppDbContext appDbContext, IMapper mapper, IQuestionUnitService questionUnitService, IPaginationService paginationService)
        {
            _appDbContext = appDbContext;
            _mapper = mapper;
            _questionUnitService = questionUnitService;
            _paginationService = paginationService;
        }

        public async Task<PaginatedModel<QuestionUnitDto>> Handle(GetQuestionUnitsQuery request, CancellationToken cancellationToken)
        {
            var questions = _appDbContext.QuestionUnits
                .Include(x => x.QuestionCategory)
                .Include(x => x.Options)
                .Include(x => x.TestQuestions)
                .Include(x => x.QuestionUnitTags)
                    .ThenInclude(x => x.Tag)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (request.Type != null)
            {
                questions = questions.Where(x => x.QuestionType == request.Type.Value);
            }

            if (request.QuestionCategoryId != null)
            {
                questions = questions.Where(x => x.QuestionCategoryId == request.QuestionCategoryId.Value);
            }

            if (!string.IsNullOrWhiteSpace(request.QuestionName))
            {
                questions = questions.Where(x => x.Question.Contains(request.QuestionName) || x.QuestionUnitTags.Any(qu => qu.Tag.Name.Contains(request.QuestionName)));
            }

            if (!string.IsNullOrWhiteSpace(request.CategoryName))
            {
                questions = questions.Where(x => x.QuestionCategory.Name.Contains(request.CategoryName));
            }

            var paginatedModel = _paginationService.MapAndPaginateModel<QuestionUnit, QuestionUnitDto>(questions, request);
            var items = paginatedModel.Items.ToList();

            var hashedQuestions = items.Where(x => x.QuestionType == QuestionTypeEnum.HashedAnswer).ToList();

            foreach (var unit in hashedQuestions)
            {
                items.Remove(unit);
                var unhashedQuestion = await _questionUnitService.GetUnHashedQuestionUnit(unit.Id);
                unhashedQuestion.Options = null;
                items.Add(_mapper.Map<QuestionUnitDto>(unhashedQuestion));
            }

            paginatedModel.Items = items;

            return paginatedModel;
        }
    }
}
