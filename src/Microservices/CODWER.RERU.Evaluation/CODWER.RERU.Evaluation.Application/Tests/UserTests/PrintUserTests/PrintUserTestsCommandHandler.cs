﻿using System.Linq;
using CVU.ERP.Common.DataTransferObjects.Files;
using MediatR;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Data.Entities;
using CODWER.RERU.Evaluation.Data.Persistence.Context;
using CODWER.RERU.Evaluation.DataTransferObjects.Tests;
using CVU.ERP.Module.Application.TablePrinterService;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserTests
{
    public class PrintUserTestsCommandHandler : IRequestHandler<PrintUserTestsCommand, FileDataDto>
    {
        private readonly AppDbContext _appDbContext;
        private readonly ITablePrinter<Test, TestDto> _printer;

        public PrintUserTestsCommandHandler(AppDbContext appDbContext, ITablePrinter<Test, TestDto> printer)
        {
            _appDbContext = appDbContext;
            _printer = printer;
        }

        public async Task<FileDataDto> Handle(PrintUserTestsCommand request, CancellationToken cancellationToken)
        {
            var userTests = _appDbContext.Tests
                .Include(t => t.TestType)
                .Include(t => t.TestQuestions)
                .Include(t => t.UserProfile)
                .Include(t => t.Location)
                .Include(t => t.Event)
                .Where(t => t.UserProfileId == request.UserId && t.Event == null)
                .OrderByDescending(x => x.ProgrammedTime)
                .AsQueryable();

            var result = _printer.PrintTable(new TableData<Test>
            {
                Name = request.TableName,
                Items = userTests,
                Fields = request.Fields,
                Orientation = request.Orientation
            });

            return result;
        }
    }
}
