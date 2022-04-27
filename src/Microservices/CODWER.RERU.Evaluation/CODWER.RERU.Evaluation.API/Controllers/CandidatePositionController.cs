﻿using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.CandidatePositions.AddCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.DeleteCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.EditCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePosition;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetCandidatePositions;
using CODWER.RERU.Evaluation.Application.CandidatePositions.GetPositionsSelectValues;
using CODWER.RERU.Evaluation.Application.CandidatePositions.PrintCandidatePosition;
using CODWER.RERU.Evaluation.DataTransferObjects.CandidatePositions;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class CandidatePositionController : BaseController
    {
        [HttpGet("{id}")]
        public async Task<CandidatePositionDto> GetCandidatePosition([FromRoute] int id)
        {
            return await Mediator.Send(new GetCandidatePositionQuery { Id = id });
        }

        [HttpGet]
        public async Task<PaginatedModel<CandidatePositionDto>> GetCandidatePositions([FromQuery] GetCandidatePositionsQuery query)
        {
            return await Mediator.Send(query);
        }


        [HttpGet("select-values")]
        public async Task<List<SelectItem>> GetCandidatePositionValue()
        {
            var query = new GetPositionsSelectValuesQuery();

            return await Mediator.Send(query);
        }

        [HttpPost]
        public async Task<int> AddEditCandidatePosition([FromBody] AddCandidatePositionCommand request)
        {
            return await Mediator.Send(request);
        }

        [HttpPatch]
        public async Task<Unit> EditCandidatePosition([FromBody] EditCandidatePositionCommand command)
        {
            var result = await Mediator.Send(command);

            return result;
        }

        [HttpDelete("{id}")]
        public async Task<Unit> DeleteCandidatePosition([FromRoute] int id)
        {
            return await Mediator.Send(new DeleteCandidatePositionCommand { Id = id });
        }

        [HttpPut("print")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> PrintCandidatePosition([FromBody] PrintCandidatePositionCommand command)
        {
            var result = await Mediator.Send(command);

            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }
    }
}