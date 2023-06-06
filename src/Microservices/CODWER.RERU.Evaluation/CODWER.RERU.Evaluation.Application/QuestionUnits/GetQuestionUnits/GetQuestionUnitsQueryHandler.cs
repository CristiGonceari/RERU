﻿using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AutoMapper;
using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.Pagination;
using MediatR;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

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

            var paginatedModel = await _paginationService.MapAndPaginateModelAsync<QuestionUnit, QuestionUnitDto>(questions, request);
            var items = paginatedModel.Items.ToList();

            var hashedQuestions = items.Where(x => x.QuestionType == QuestionTypeEnum.HashedAnswer).ToList();

            foreach (var unit in hashedQuestions)
            {
                var index = items.FindIndex(x => x.Id == unit.Id);
                var unhashedQuestion = await _questionUnitService.GetUnHashedQuestionUnit(unit.Id);
                items[index] = _mapper.Map<QuestionUnitDto>(unhashedQuestion);
            }

            paginatedModel.Items = items;

            return paginatedModel;
        }
    }
}
