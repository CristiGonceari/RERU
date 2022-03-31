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
            var filterData = new QuestionFilterDto
            {
                QuestionName = request.QuestionName,
                CategoryName = request.CategoryName,
                QuestionCategoryId = request.QuestionCategoryId,
                QuestionTags = request.QuestionTags,
                Type = request.Type,
                Status = request.Status
            };

            var questions = GetAndFilterQuestionUnits.Filter(_appDbContext, filterData);

            questions = SelectOnlyReturnedFields(questions);

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<QuestionUnit, QuestionUnitDto>(questions, request);
            var items = paginatedModel.Items.ToList();

            var hashedQuestions = items.Where(x => x.QuestionType == QuestionTypeEnum.HashedAnswer).ToList();

            foreach (var unit in hashedQuestions)
            {
                items.Remove(unit);
                var unhashedQuestion = await _questionUnitService.GetUnHashedQuestionUnit(unit.Id);
                items.Add(_mapper.Map<QuestionUnitDto>(unhashedQuestion));
            }

            paginatedModel.Items = items;

            return paginatedModel;
        }

        private IQueryable<QuestionUnit> SelectOnlyReturnedFields(IQueryable<QuestionUnit> items)
        {
            return items
                .Select(x => new QuestionUnit
                {
                    Id = x.Id,
                    QuestionType = x.QuestionType,
                    Status = x.Status,
                    Question = x.Question,
                    QuestionPoints = x.QuestionPoints,
                    MediaFileId = x.MediaFileId,
                    QuestionCategory = new QuestionCategory
                    {
                        Id = x.QuestionCategoryId,
                        Name = x.QuestionCategory.Name,
                    },
                    TestQuestions = x.TestQuestions.Select(tq => new TestQuestion()).ToList(),
                    Options = x.Options.Select(o => new Option
                    {
                        IsCorrect = o.IsCorrect
                    }).ToList(),
                    QuestionUnitTags = x.QuestionUnitTags.Select(qut => new QuestionUnitTag
                    {
                        Tag = new Tag
                        {
                            Id = qut.TagId,
                            Name = qut.Tag.Name
                        }
                    }).ToList()
                });
        }
    }
}
