using CODWER.RERU.Evaluation.Application.Services;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Entities.Enums;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionUnits;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.QuestionUnits.PrintQuestionUnits
{
    public class PrintQuestionUnitsCommandHandler : IRequestHandler<PrintQuestionUnitsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<QuestionUnit, QuestionUnitDto> _printer;
        private readonly IQuestionUnitService _questionUnitService;

        public PrintQuestionUnitsCommandHandler(AppDbContext appDbContext, ITablePrinter<QuestionUnit, QuestionUnitDto> printer, IQuestionUnitService questionUnitService)
        {
            _appDbContext = appDbContext;
            _questionUnitService = questionUnitService;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintQuestionUnitsCommand request, CancellationToken cancellationToken)
        {
            var questions = GetAndFilterQuestionUnits
                    .Filter(_appDbContext, request.QuestionName, request.CategoryName, request.QuestionTags, request.Type, request.Status);

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

            var result = _printer.PrintTable(new TableData<QuestionUnit>
            {
                Name = request.TableName,
                Items = questions,
                Fields = request.Fields,
                Orientation = request.Orientation
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
