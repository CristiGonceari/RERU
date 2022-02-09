﻿using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.QuestionCategory;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.QuestionCategories.PrintQuestionCategories
{
    public class PrintQuestionCategoriesCommandHandler : IRequestHandler<PrintQuestionCategoriesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<QuestionCategory, QuestionCategoryDto> _printer;

        public PrintQuestionCategoriesCommandHandler(AppDbContext appDbContext, ITablePrinter<QuestionCategory, QuestionCategoryDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintQuestionCategoriesCommand request, CancellationToken cancellationToken)
        {
            var categories = GetAndFilterQuestionCategories.Filter(_appDbContext, request.Name);

            var result = _printer.PrintTable(new TableData<QuestionCategory>
            { 
                Name = request.TableName,
                Items = categories,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;

        }
    }
}
