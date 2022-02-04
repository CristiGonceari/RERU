﻿using CVU.ERP.Common.DataTransferObjects.Files;
using CVU.ERP.Module.Application.TablePrinterService;
using MediatR;

namespace CODWER.RERU.Evaluation.Application.Tests.UserTests.PrintUserEvaluatedTests
{
    public class PrintUserEvaluatedTestsCommand : TableParameter, IRequest<FileDataDto>
    {
        public int UserId { get; set; }
    }
}