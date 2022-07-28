using System.Collections.Generic;
using CODWER.RERU.Evaluation.API.Config;
using CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.AddSolicitedVacantPositionUserFile;
using CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFile;
using CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetSolicitedVacantPositionUserFiles;
using CVU.ERP.Common.DataTransferObjects.SelectValues;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.DeleteSolicitedVacantPositionUserFile;
using CODWER.RERU.Evaluation.DataTransferObjects.Documents;
using CODWER.RERU.Evaluation.DataTransferObjects.SolicitedPositions;
using MediatR;

namespace CODWER.RERU.Evaluation.API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class SolicitedVacantPositionUserFileController : BaseController 
    {
        [HttpGet("{fileId}")]
        [IgnoreResponseWrap]
        public async Task<IActionResult> GetUserFile([FromRoute] string fileId)
        {
            var query = new GetSolicitedVacantPositionUserFileQuery { FileId = fileId };

            var result = await Mediator.Send(query);
            Response.Headers.Add("Access-Control-Expose-Headers", "Content-Disposition");

            return File(result.Content, result.ContentType, result.Name);
        }

        [HttpGet("files")]
        public async Task<List<GetSolicitedVacantPositionDto>> GetUserFiles([FromQuery] GetSolicitedVacantPositionUserFilesQuery query)
        {
            var result = await Mediator.Send(query);

            return result;
        }

        [HttpPost]
        public async Task<string> AddUserFile([FromForm] AddSolicitedVacantPositionUserFileCommand command)
        {
            return await Mediator.Send(command);
        }

        [HttpDelete("{fileId}")]
        public async Task<Unit> DeleteFile([FromRoute] string fileId)
        {
            var command = new DeleteSolicitedVacantPositionUserFileCommand { FileId = fileId };

            var result = await Mediator.Send(command);

            return result;
        }
    }
}
