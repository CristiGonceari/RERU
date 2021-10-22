using System;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;
using CVU.ERP.Module.Application;
using CVU.ERP.Module.Application.ExceptionHandlers;
using CVU.ERP.Module.Application.Exceptions;
using FluentAssertions;
using Xunit;

namespace tests.ExceptionHandlers {
    public class ApplicationUnauthenticatedResponseExceptionHandlerTests {
        private readonly ApplicationUnauthenticatedResponseExceptionHandler _testObject;
        public ApplicationUnauthenticatedResponseExceptionHandlerTests () {
            _testObject = new ApplicationUnauthenticatedResponseExceptionHandler ();
        }

        [Fact]
        public async Task Handle_Returns_401 () {
            //Given
            var givenException = new ApplicationUnauthenticatedException ();
            var givenResponse = new Response ();
            //When
            var response = await _testObject.Handle (givenException, givenResponse);
            //Then
            response.Should ().Be (401);
        }

        [Fact]
        public async Task Handle_Returns_401_as_Exception () {
            //Given
            Exception givenException = new ApplicationUnauthenticatedException ();
            var givenResponse = new Response ();
            //When
            var response = await _testObject.Handle (givenException, givenResponse);
            //Then
            response.Should ().Be (401);
        }

        [Fact]
        public async Task Handle_Writes_Message_Code_In_Response_As_ErrorMessage () {
            //Given
            var givenException = new ApplicationUnauthenticatedException ();
            var givenResponse = new Response ();
            //When
            await _testObject.Handle (givenException, givenResponse);
            //Then
            givenResponse.Messages.Should ().HaveCount (1);
            var message = givenResponse.Messages.First ();
            message.Type.Should ().Be (MessageType.Error);
            message.Code.Should ().Be (ApplicationMessageCodes.APPLICATION_UNAUTHENTICATED);
        }
    }
}