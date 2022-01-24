using MediatR;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace CODWER.RERU.Evaluation.Application.Options.BulkUploadOptions
{
    public class BulkUploadOptionsCommand : IRequest<byte[]>
    {
        public IFormFile Input { get; set; }
        public int QuestionUnitId { get; set; }

    }
}
