﻿using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.VerificationTests.FinalizeTestVerification;
using CODWER.RERU.Evaluation.Application.VerificationTests.GetTestVerificationSummary;
using CODWER.RERU.Evaluation.Application.VerificationTests.GetVerificationTestQuestion;
using CODWER.RERU.Evaluation.Application.VerificationTests.SetTestQuestionAsVerified;
using CODWER.RERU.Evaluation.DataTransferObjects.VerificationTests;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TestVerificationController : BaseController
    {
        [HttpGet]
        public async Task<VerificationTestQuestionUnitDto> GetTestQuestion([FromQuery] GetTestQuestionForVerifyQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpGet("summary")]
        public async Task<VerificationTestQuestionDataDto> GetTestQuestionsSummary([FromRoute] GetTestVerificationSummaryQuery query)
        {
            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<Unit> SetVerifiedTestQuestion([FromBody] SetTestQuestionAsVerifiedCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpPost("verified")]
        public async Task<Unit> SetTestAsVerified([FromBody] FinalizeTestVerificationCommand command)
        {
            return await Mediator.Send(command);
        }
    }
}
