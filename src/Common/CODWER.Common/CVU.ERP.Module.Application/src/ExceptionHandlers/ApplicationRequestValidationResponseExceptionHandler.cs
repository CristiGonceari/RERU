using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.ServiceProvider.Exceptions;

namespace CVU.ERP.Module.Application.ExceptionHandlers
{
    public class ApplicationRequestValidationResponseExceptionHandler : IResponseExceptionHandler<ApplicationRequestValidationException>
    {

        public Task<int> Handle(ApplicationRequestValidationException exception, Response response)
        {
            var errorMessages = exception.ValidationMessages
                .Select(vm => new ErrorMessage(vm.MessageText, vm.Code));
            
            response.AddErrorMessages(errorMessages); 
            
            return Task.FromResult((int)HttpStatusCode.BadRequest);
        }

        public Task<int> Handle(Exception exception, Response response)
        {
            return Handle(exception as ApplicationRequestValidationException, response);
        }
    }
}