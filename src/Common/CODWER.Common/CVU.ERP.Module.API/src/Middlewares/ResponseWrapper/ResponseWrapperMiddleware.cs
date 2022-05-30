using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;
using CVU.ERP.Module.API.Contstants;
using CVU.ERP.Module.API.Extensions;
using CVU.ERP.Module.API.Extensions.Strings;
using CVU.ERP.Module.API.Helpers;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper.Attributes;
using CVU.ERP.Module.Common.ExceptionHandlers;
using CVU.ERP.Module.Common.MessageCodes;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using static Microsoft.AspNetCore.Http.StatusCodes;

namespace CVU.ERP.Module.API.Middlewares.ResponseWrapper
{
    public class ResponseWrapperMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ResponseWrapperOptions _options;
        private readonly ILogger<ResponseWrapperMiddleware> _logger;
        private JsonSerializerSettings _jsonSettings;
        private IEnumerable<IResponseExceptionHandler> _exceptionHandlers;
        public ResponseWrapperMiddleware(RequestDelegate next,
            ResponseWrapperOptions options,
            ILogger<ResponseWrapperMiddleware> logger,
            IEnumerable<IResponseExceptionHandler> exceptionHandlers)
        {
            _next = next;
            _options = options;
            _logger = logger;
            _jsonSettings = JsonHelper.GetJsonSettings(options.IgnoreNullValue);
            _exceptionHandlers = exceptionHandlers;
        }

        public virtual async Task InvokeAsync(HttpContext context)
        {
            if (IsSwaggerRequest(context, _options.SwaggerPath) || !IsApiRequest(context))
                await _next(context);
            else
            {
                var originalResponseBodyStream = context.Response.Body;

                using var memoryStream = new MemoryStream();

                try
                {
                    context.Response.Body = memoryStream;
                    await _next.Invoke(context);

                    if (context.Response.HasStarted) { LogResponseHasStartedError(); return; }

                    var endpoint = context.GetEndpoint();
                    if (endpoint?.Metadata?.GetMetadata<IgnoreResponseWrapAttribute>() is object)
                    {
                        await RevertResponseBodyStreamAsync(memoryStream, originalResponseBodyStream);
                        return;
                    }

                    var bodyAsText = await ReadResponseBodyStreamAsync(memoryStream);
                    context.Response.Body = originalResponseBodyStream;

                    if (context.Response.StatusCode == Status204NoContent)
                    {
                        if (endpoint?.Metadata?.GetMetadata<IgnoreEmptyContentResponseWrapAttribute>() is object)
                        {
                            await RevertResponseBodyStreamAsync(memoryStream, originalResponseBodyStream);
                            return;
                        }
                        else
                        {
                            await HandleEmptyContentAsNotFound(context);
                            return;
                        }
                    }

                    // if (context.Response.StatusCode != Status304NotModified) {

                    if (!_options.IsApiOnly && (bodyAsText.IsHtml() && !_options.BypassHTMLValidation) && context.Response.StatusCode == Status200OK)
                    {
                        context.Response.StatusCode = Status404NotFound;
                    }

                    if (!context.Request.Path.StartsWithAnySegments(_options.WrapWhenApiPathStartsWith) && (bodyAsText.IsHtml() && !_options.BypassHTMLValidation) && context.Response.StatusCode == Status200OK)
                    {
                        if (memoryStream.Length > 0) { await HandleNotApiRequestAsync(context); } // Atunci cand se returneaza un HTML invalid
                        return;
                    }

                    var isRequestOk = IsRequestSuccessful(context.Response.StatusCode);
                    if (isRequestOk)
                    {
                        await HandleSuccessfulRequestAsync(context, bodyAsText, context.Response.StatusCode);
                    }
                    else
                    {
                        await HandleUnsuccessfulRequestAsync(context, bodyAsText, context.Response.StatusCode);
                    }
                    // }

                }
                catch (Exception exception)
                {
                    if (context.Response.HasStarted)
                    {
                        LogResponseHasStartedError();
                        return;
                    }
                    await HandleExceptionAsync(context, exception);
                    await RevertResponseBodyStreamAsync(memoryStream, originalResponseBodyStream);
                }
            }
        }

        private void LogResponseHasStartedError()
        {
            _logger.Log(LogLevel.Warning, "The response has already started, the ResponseWrapper middleware will not be executed.");
        }

        private async Task<string> ReadResponseBodyStreamAsync(Stream bodyStream)
        {
            bodyStream.Seek(0, SeekOrigin.Begin);
            var responseBody = await new StreamReader(bodyStream).ReadToEndAsync();
            bodyStream.Seek(0, SeekOrigin.Begin);

            var (IsEncoded, ParsedText) = responseBody.VerifyBodyContent();

            return IsEncoded ? ParsedText : responseBody;
        }

        private async Task HandleNotApiRequestAsync(HttpContext context)
        {
            var response = new Response();
            response.AddErrorMessage(CommonMessageCodes.NOT_API_REQUEST);
            await WriteResponseToHttpContextAsync(context, context.Response.StatusCode, response);
        }

        private bool IsRequestSuccessful(int statusCode)
        {
            return (statusCode >= 200 && statusCode < 400);
        }

        private async Task HandleEmptyContentAsNotFound(HttpContext context)
        {
            var response = new Response();
            response.AddErrorMessage(new ErrorMessage(CommonMessageCodes.ROUTE_NOT_FOUND));
            var jsonString = ConvertToJSONString(response);
            await WriteFormattedResponseToHttpContextAsync(context, Status404NotFound, jsonString);
        }

        private async Task HandleSuccessfulRequestAsync(HttpContext context, object body, int httpStatusCode)
        {
            var bodyText = !body.ToString().IsValidJson() ? ConvertToJSONString(body) : body.ToString();

            var bodyContent = JsonConvert.DeserializeObject<object>(bodyText);
            // Type type = bodyContent?.GetType ();

            // string jsonString;
            // if (type.Equals (typeof (JObject))) {
            //     var response = new Response<object> (result);
            //     jsonString = ConvertToJSONString (bodyContent);
            // } else {
            //     var validated = ValidateSingleValueType (bodyContent);
            //     object result = validated.Item1 ? validated.Item2 : bodyContent;
            //     var response = new Response<object> (result);
            //     jsonString = ConvertToJSONString (response);
            // }
            var response = new Response<object>(bodyContent);
            await WriteResponseToHttpContextAsync(context, httpStatusCode, response);
        }

        private string ConvertToJSONString(Response apiResponse)
        {
            return JsonConvert.SerializeObject(apiResponse, _jsonSettings);
        }

        private string ConvertToJSONString(object rawJSON) => JsonConvert.SerializeObject(rawJSON, _jsonSettings);

        private async Task HandleUnsuccessfulRequestAsync(HttpContext context, object body, int httpStatusCode)
        {
            var (IsEncoded, ParsedText) = body.ToString().VerifyBodyContent();
            var bodyText = IsEncoded ? JsonConvert.DeserializeObject<dynamic>(ParsedText) : body.ToString();

            Response errorResponse = new Response();
            if (!string.IsNullOrEmpty(body.ToString()))
            {
                errorResponse.AddErrorMessage(body.ToString());
            }
            else
            {
                errorResponse.AddErrorMessage(GetDefaultErrorMessageForStatusCode(httpStatusCode));
            }
            await WriteResponseToHttpContextAsync(context, httpStatusCode, errorResponse);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var response = new Response();
            var httpStatusCode = StatusCodes.Status500InternalServerError;

            var exceptionHandler = _exceptionHandlers.FirstOrDefault(
                eh => eh.GetType()
                        .GetInterfaces()
                        .Any(i => i.IsGenericType && i.GenericTypeArguments.Any(gta => gta == exception.GetType()))
            );
            if (exceptionHandler != null)
            {
                httpStatusCode = await exceptionHandler.Handle(exception, response);
            }
            else
            {
                var errorMessage = new ErrorMessage(exception.Message);
                //TODO: De adaugat un if si configuratie, daca de adaugat stack trace sau nu.
                errorMessage.Data.Add(new KeyValuePair<string, object>("StackTrace", exception.StackTrace));
                if (exception.InnerException != null)
                {
                    errorMessage.Data.Add(new KeyValuePair<string, object>("InnerExceptionStackTrace", exception.InnerException.StackTrace));
                    errorMessage.Data.Add(new KeyValuePair<string, object>("InnerExceptionMessage", exception.InnerException.Message));
                }

                response.AddErrorMessage(errorMessage);
            }
            await WriteResponseToHttpContextAsync(context, httpStatusCode, response);
        }

        private async Task WriteResponseToHttpContextAsync(HttpContext context, int httpStatusCode, Response response)
        {
            var jsonString = ConvertToJSONString(response);
            await WriteFormattedResponseToHttpContextAsync(context, httpStatusCode, jsonString);
        }

        // rescriere response-ul curent cu cel nou.
        private async Task WriteFormattedResponseToHttpContextAsync(HttpContext context, int httpStatusCode, string jsonString)
        {
            context.Response.StatusCode = httpStatusCode;
            context.Response.ContentType = HttpContentMediaTypes.JSONHttpContentMediaType;
            context.Response.ContentLength = jsonString != null ? Encoding.UTF8.GetByteCount(jsonString) : 0;
            await context.Response.WriteAsync(jsonString);
        }

        private string GetDefaultErrorMessageForStatusCode(int statusCode)
        {
            switch (statusCode)
            {
                case Status204NoContent:
                    return CommonMessageCodes.NO_CONTENT;
                case Status400BadRequest:
                    return CommonMessageCodes.BAD_REQUEST;
                case Status401Unauthorized:
                    return CommonMessageCodes.UNAUTHORIZED_REQUEST;
                case Status404NotFound:
                    return CommonMessageCodes.ROUTE_NOT_FOUND;
                case Status405MethodNotAllowed:
                    return CommonMessageCodes.METHOD_NOT_ALLOWED;
                case Status415UnsupportedMediaType:
                    return CommonMessageCodes.UNSUPORTED_MEDIA_TYPE;
                default:
                    return CommonMessageCodes.GENERAL_UNKNOWN;
            };
        }

        public async Task RevertResponseBodyStreamAsync(Stream bodyStream, Stream orginalBodyStream)
        {
            bodyStream.Seek(0, SeekOrigin.Begin);
            await bodyStream.CopyToAsync(orginalBodyStream);
        }

        private (bool, object) ValidateSingleValueType(object value)
        {
            var result = value.ToString();
            if (result.IsWholeNumber()) { return (true, result.ToInt64()); }
            if (result.IsDecimalNumber()) { return (true, result.ToDecimal()); }
            if (result.IsBoolean()) { return (true, result.ToBoolean()); }

            return (false, value);
        }

        // se controleaza pentru swagger
        private bool IsSwaggerRequest(HttpContext context, string swaggerPath)
        {
            return context.Request.Path.StartsWithSegments(new PathString(swaggerPath));
        }

        // se controleaza daca requestul pentru sa fie doar api
        private bool IsApiRequest(HttpContext context)
        {
            if (_options.IsApiOnly && !context.Request.Path.Value.Contains(".js") && !context.Request.Path.Value.Contains(".css"))
                return true;

            return context.Request.Path.StartsWithAnySegments(_options.WrapWhenApiPathStartsWith);
        }
    }
}