using System;
using System.Net;
using CVU.ERP.Common.ErrorHandling;
using CVU.ERP.Common.Interfaces;
using CVU.ERP.Infrastructure.Filters.Exceptions;
using CVU.ERP.Infrastructure.Util;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Filters;
using Newtonsoft.Json;

namespace CVU.ERP.Infrastructure.Filters
{
    public class CustomExceptionFilter : IExceptionFilter
    {
        private readonly IEmailService emailService;

        public CustomExceptionFilter(IEmailService emailService)
        {
            this.emailService = emailService;
        }

        public virtual void OnException(ExceptionContext context)
        {
            HttpStatusCode status;
            string message;
            object validation = null;

            var exceptionType = context.Exception.GetType();
            if (exceptionType == typeof(UnauthorizedAccessException))
            {
                message = "Unauthorized Access.";
                status = HttpStatusCode.Unauthorized;
            }
            else if (context.Exception is ValidationException)
            {
                status = HttpStatusCode.BadRequest;
                validation = ((ValidationException)context.Exception).Failures;
                message = "Validation failed.";
                status = HttpStatusCode.BadRequest;
            }
            else if (exceptionType == typeof(NotImplementedException))
            {
                message = "A server error occurred.";
                status = HttpStatusCode.NotImplemented;
            }
            else if (exceptionType == typeof(AppException))
            {
                message = context.Exception.Message;
                status = ((AppException)context.Exception).HttpStatusCode;
            }
            else
            {
                message = "A server error occurred.";
                status = HttpStatusCode.InternalServerError;

                emailService.AddSubject("Internal Error")
                            .AddBody(EmailTableFormat.ErrorToString(context.HttpContext, context.Exception, string.Empty))
                            .Send();
            }

            HttpResponse response = context.HttpContext.Response;
            response.StatusCode = (int)status;
            response.ContentType = "application/json";

            var responseObj = new ErrorResponse
            {
                Message = message,
                Validation = validation,
#if DEBUG
                Detail = $"{context.Exception?.Message} {context.Exception?.StackTrace} {context.Exception?.InnerException?.Message} {context.Exception?.InnerException?.StackTrace}"
#endif
            };

            response.WriteAsync(JsonConvert.SerializeObject(responseObj));
        }
    }
}
