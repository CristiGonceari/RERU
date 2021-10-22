using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.API.Middlewares.ResponseWrapper;
using CVU.ERP.Module.Common.ExceptionHandlers;
using FluentAssertions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using NSubstitute;
using Xunit;

namespace tests.Middlewares.ResponseWrapper {

    public class MockException : Exception {

    }

    public class AnotherMockException : Exception {

    }

    public class ResponseWrapperMiddlewareTests {
        private ResponseWrapperMiddleware _testObject;
        private readonly ILogger<ResponseWrapperMiddleware> _logger;
        private readonly IResponseExceptionHandler<MockException> _mockExceptionHandler;
        private readonly IResponseExceptionHandler<AnotherMockException> _anotherMockExceptionHandler;

        public ResponseWrapperMiddlewareTests () {

            _mockExceptionHandler = Substitute.For<IResponseExceptionHandler<MockException>> ();
            _anotherMockExceptionHandler = Substitute.For<IResponseExceptionHandler<AnotherMockException>> ();
        }

        [Fact]
        public async Task Exceptions_Should_Be_Handled_By_Specified_Handlers () {

            var defaultContext = new DefaultHttpContext ();
            defaultContext.Response.Body = new MemoryStream ();
            defaultContext.Request.Path = "/";

            var givenStatusCode = 499;
            var givenException = new MockException ();
            RequestDelegate next = (innerHttpContext) => {
                throw givenException;
            };

            _mockExceptionHandler.Handle (Arg.Is<Exception> (e => e == givenException), Arg.Any<Response> ()).Returns (givenStatusCode);

            var list = new List<IResponseExceptionHandler> () { _anotherMockExceptionHandler, _mockExceptionHandler };

            _testObject = new ResponseWrapperMiddleware (next,
                new ResponseWrapperOptions (),
                _logger,
                list);
            await _testObject.InvokeAsync (defaultContext);
            await _mockExceptionHandler.Received (1).Handle (Arg.Is<Exception> (e => e == givenException), Arg.Any<Response> ());
            defaultContext.Response.StatusCode.Should ().Be (givenStatusCode);
        }
    }
}