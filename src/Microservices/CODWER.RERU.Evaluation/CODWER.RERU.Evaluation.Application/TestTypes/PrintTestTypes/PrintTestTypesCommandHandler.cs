﻿using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.TestTypes;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.Application.TestTypes.PrintTestTypes
{
    public class PrintTestTypesCommandHandler : IRequestHandler<PrintTestTypesCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<TestType, TestTypeDto> _printer;

        public PrintTestTypesCommandHandler(AppDbContext appDbContext, ITablePrinter<TestType, TestTypeDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintTestTypesCommand request, CancellationToken cancellationToken)
        {
            var testTypes = _appDbContext.TestTypes
                .Include(x => x.TestTypeQuestionCategories)
                .ThenInclude(x => x.QuestionCategory)
                .Include(x => x.EventTestTypes)
                .ThenInclude(x => x.Event)
                .OrderByDescending(x => x.CreateDate)
                .AsQueryable();

            if (!string.IsNullOrEmpty(request.Name))
            {
                testTypes = testTypes.Where(x => x.Name.Contains(request.Name));
            }

            if (request.Status.HasValue)
            {
                testTypes = testTypes.Where(x => x.Status == request.Status);
            }

            if (!string.IsNullOrEmpty(request.EventName))
            {
                testTypes = testTypes.Where(x => x.EventTestTypes.Any(x => x.Event.Name.Contains(request.EventName)));
            }

            var result = _printer.PrintTable(new TableData<TestType>
            {
                Name = request.TableName,
                Items = testTypes,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}