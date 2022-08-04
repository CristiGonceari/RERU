using System;
using System.Net;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.ServiceProvider.Exceptions;

namespace src.ExceptionHandlers
{
    public class CoreClientResponseNotSuccessfulExceptionHandler : IResponseExceptionHandler<CoreClientResponseNotSuccessfulException>
    {
        public Task<int> Handle(CoreClientResponseNotSuccessfulException exception, Response response)
        {
            response.AddErrorMessages(exception.Messages);
            return Task.FromResult((int)HttpStatusCode.BadRequest);
        }

        public Task<int> Handle(Exception exception, Response response)
        {
            return Handle(exception as CoreClientResponseNotSuccessfulException, response);
        }
    }
}