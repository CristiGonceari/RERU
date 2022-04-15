using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.PrintQuestionCategories
{
    public class PrintQuestionCategoriesCommandHandler : IRequestHandler<PrintQuestionCategoriesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<QuestionCategory, QuestionCategoryDto> _printer;

        public PrintQuestionCategoriesCommandHandler(AppDbContext appDbContext, IExportData<QuestionCategory, QuestionCategoryDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintQuestionCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categories = GetAndFilterQuestionCategories.Filter(_appDbContext, request.Name);

            var result = _printer.ExportTableSpecificFormat(new TableData<QuestionCategory>
            { 
                Name = request.TableName,
                Items = categories,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;

        }
    }
}
