﻿using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TableExportServices;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities;
using RERU.Data.Entities.Enums;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserEvaluatedTests
{
    public class PrintUserEvaluatedTestsCommandHandler : IRequestHandler<PrintUserEvaluatedTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly IExportData<Test, TestDto> _printer;

        public PrintUserEvaluatedTestsCommandHandler(AppDbContext appDbContext, IExportData<Test, TestDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserEvaluatedTestsCommand request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestTemplate)
                .Include(t => t.UserProfile)
                .Include(t => t.Event)
                .Where(t => (t.EvaluatorId == request.UserId ||
                             _appDbContext.EventEvaluators.Any(x => x.EventId == t.EventId && x.EvaluatorId == request.UserId)) && 
                            t.TestTemplate.Mode == TestTemplateModeEnum.Test)
                .AsQueryable();

            var result = _printer.ExportTableSpecificFormat(new TableData<Test>
            {
                Name = request.TableName,
                Items = userTests,
                Fields = request.Fields,
                Orientation = request.Orientation,
                ExportFormat = request.TableExportFormat
            });

            return result;
        }
    }
}
