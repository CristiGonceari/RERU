using System;
using System.Net;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Application.Exceptions;
using CVU.ERP.Module.Common.ExceptionHandlers;

namespace CVU.ERP.Module.Application.ExceptionHandlers {
    public class ApplicationUnauthorizedResponseExceptionHandler : IResponseExceptionHandler<ApplicationUnauthorizedException> {
        public Task<int> Handle (ApplicationUnauthorizedException exception, Response response) {
            response.AddErrorMessage ("Unauthorized", ApplicationMessageCodes.APPLICATION_UNAUTHORIZED);
            return Task.FromResult ((int) HttpStatusCode.Forbidden);
        }

        public Task<int> Handle (Exception exception, Response response) {
            return Handle (exception as ApplicationUnauthorizedException, response);
        }
    }
}