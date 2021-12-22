﻿using System.Collections.Generic;
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
                .OrderByDescending(x => x.Id)
                .Select(x=> new QuestionUnit
                {
                    Id = x.Id,
                    QuestionType = x.QuestionType,
                    Status = x.Status,
                    Question = x.Question,
                    QuestionPoints = x.QuestionPoints,
                    MediaFileId = x.MediaFileId,
                    QuestionCategory = new QuestionCategory
                    {
                        Id = x.QuestionCategory.Id,
                        Name = x.QuestionCategory.Name,
                    },

                    Options = x.Options.Select(o => new Option()).ToList(),
                    TestQuestions = x.TestQuestions.Select(tq => new TestQuestion()).ToList(),
                    QuestionUnitTags = x.QuestionUnitTags.Select(qut => new QuestionUnitTag
                    {
                        Tag = new Tag
                        {
                            Name = qut.Tag.Name
                        }
                    }).ToList()
                })
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

            IsReadyToActivate(items, questions);

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

        private void IsReadyToActivate(List<QuestionUnitDto> items, IQueryable<QuestionUnit> questions)
        {
            foreach (var item in items)
            {
                var options = questions.First(q => q.Id == item.Id).Options.Any(x => x.IsCorrect);

                if (item.OptionsCount > 1 && options || item.QuestionType == QuestionTypeEnum.FreeText || item.QuestionType == QuestionTypeEnum.HashedAnswer)
                {
                    item.IsReadyToActivate = true;
                }

            }
        }

    }
}
