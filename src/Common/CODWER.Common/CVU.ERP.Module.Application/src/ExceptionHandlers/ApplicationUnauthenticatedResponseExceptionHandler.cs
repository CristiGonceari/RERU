using System;
using System.Net;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.ServiceProvider.Exceptions;

namespace CVU.ERP.Module.Application.ExceptionHandlers
{
    public class ApplicationUnauthenticatedResponseExceptionHandler : IResponseExceptionHandler<ApplicationUnauthenticatedException>
    {
        public Task<int> Handle(ApplicationUnauthenticatedException exception, Response response)
        {
            response.AddErrorMessage(string.Empty, ApplicationMessageCodes.APPLICATION_UNAUTHENTICATED);
            return Task.FromResult((int)HttpStatusCode.Unauthorized);
        }

        public Task<int> Handle(Exception exception, Response response)
        {
            return Handle(exception as ApplicationUnauthenticatedException, response);
        }
    }
}