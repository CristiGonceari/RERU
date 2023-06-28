using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.PrintQuestionUnits
{
    public class PrintQuestionUnitsCommandHandler : IRequestHandler<PrintQuestionUnitsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<QuestionUnit, QuestionUnitDto> _printer;
        private readonly IQuestionUnitService _questionUnitService;

        public PrintQuestionUnitsCommandHandler(AppDbContext appDbContext, IExportData<QuestionUnit, QuestionUnitDto> printer, IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _questionUnitService = questionUnitService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintQuestionUnitsCommand request, CancellationToken cancellationToken)
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

            var hashedQuestions = questions.Where(x => x.QuestionType == QuestionTypeEnum.HashedAnswer).ToList();

            if(hashedQuestions != null) 
            { 
                foreach (var unit in hashedQuestions)
                {
                    var unhashedQuestion = await _questionUnitService.GetUnHashedQuestionUnit(unit.Id);
                    unhashedQuestion.Options = null;
                }
            }

            var result = _printer.ExportTableSpecificFormat(new TableData<QuestionUnit>
            {
                Name = request.TableName,
                Items = questions,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
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
                    MediaFileId = x.MediaFileId,
                    Options = x.Options,
                    QuestionCategoryId = x.QuestionCategoryId,
                    QuestionCategory = new QuestionCategory
                    {
                        Id = x.QuestionCategoryId,
                        Name = x.QuestionCategory.Name,
                    },
                    QuestionUnitTags = x.QuestionUnitTags.Select(qut => new QuestionUnitTag
                    {
                        Tag = new Tag
                        {
                            Id = qut.TagId,
                            Name = qut.Tag.Name
                        }
                    }).ToList()
                })
                .AsQueryable();
        }
    }
}
