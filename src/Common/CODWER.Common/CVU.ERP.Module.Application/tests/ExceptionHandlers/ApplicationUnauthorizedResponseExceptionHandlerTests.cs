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
    public class ApplicationUnauthorizedResponseExceptionHandlerTests {
        private readonly ApplicationUnauthorizedResponseExceptionHandler _testObject;

        public ApplicationUnauthorizedResponseExceptionHandlerTests () {
            _testObject = new ApplicationUnauthorizedResponseExceptionHandler ();
        }

        [Fact]
        public async Task Handle_Returns_403 () {
            //Given
            var givenException = new ApplicationUnauthorizedException ();
            var givenResponse = new Response ();
            //When
            var response = await _testObject.Handle (givenException, givenResponse);
            //Then
            response.Should ().Be (403);
        }

        [Fact]
        public async Task Handle_Returns_403_as_Exception () {
            //Given
            Exception givenException = new ApplicationUnauthorizedException ();
            var givenResponse = new Response ();
            //When
            var response = await _testObject.Handle (givenException, givenResponse);
            //Then
            response.Should ().Be (403);
        }

        [Fact]
        public async Task Handle_Writes_Message_Code_In_Response_As_ErrorMessage () {
            //Given
            var givenException = new ApplicationUnauthorizedException ();
            var givenResponse = new Response ();
            //When
            await _testObject.Handle (givenException, givenResponse);
            //Then
            givenResponse.Messages.Should ().HaveCount (1);
            var message = givenResponse.Messages.First ();
            message.Type.Should ().Be (MessageType.Error);
            message.Code.Should ().Be (ApplicationMessageCodes.APPLICATION_UNAUTHORIZED);
        }
    }
}