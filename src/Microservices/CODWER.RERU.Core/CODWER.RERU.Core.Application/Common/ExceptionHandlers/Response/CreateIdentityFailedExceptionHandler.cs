using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;
using CODWER.RERU.Core.Application.Common.Services.Identity.Exceptions;
using CVU.ERP.Module.Common.ExceptionHandlers;

namespace CODWER.RERU.Core.Application.Common.ExceptionHandlers.Response
{
    public class CreateIdentityFailedExceptionHandler : IResponseExceptionHandler<CreateIdentityFailedException>
    {
        public Task<int> Handle(CreateIdentityFailedException exception, CVU.ERP.Common.DataTransferObjects.Response.Response response)
        {
            var errorMessages = exception.Errors
               .Select(em => new ErrorMessage(em));

            response.AddErrorMessages(errorMessages);

            return Task.FromResult((int)HttpStatusCode.BadRequest);
        }

        public Task<int> Handle(Exception exception, CVU.ERP.Common.DataTransferObjects.Response.Response response)
        {
            return Handle(exception as CreateIdentityFailedException, response);
        }
    }
}