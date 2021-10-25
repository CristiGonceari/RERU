using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;
using CVU.ERP.Module.Application.ExceptionHandlers;
using CVU.ERP.Module.Application.Exceptions;
using FluentAssertions;
using Xunit;

namespace tests.ExceptionHandlers {
    public class ApplicationRequestValidationResponseExceptionHandlerTests {
        private ApplicationRequestValidationResponseExceptionHandler _testObject;
        public ApplicationRequestValidationResponseExceptionHandlerTests () {
            _testObject = new ApplicationRequestValidationResponseExceptionHandler ();
        }

        [Fact]
        public async Task Handle_Returns_400 () {
            var givenException = new ApplicationRequestValidationException (new List<ValidationMessage>());
            var givenResponse = new Response ();
            var result = await _testObject.Handle (givenException, givenResponse);

            result.Should ().Be (400);
        }

        [Fact]
        public async Task Handle_Returns_400_For_Exception () {
            Exception givenException = new ApplicationRequestValidationException (new List<ValidationMessage>());
            var givenResponse = new Response ();
            var result = await _testObject.Handle (givenException, givenResponse);

            result.Should ().Be (400);
        }

        [Fact]
        public async Task Handle_Writes_Messages_In_Response ()
        {
            var givenMessages = new List<ValidationMessage>()
            {
                new ValidationMessage
                {
                    MessageText = "first message",
                },
                new ValidationMessage
                {
                    MessageText = "second message"
                }
            };
            var givenException = new ApplicationRequestValidationException (givenMessages);
            var givenResponse = new Response ();

            await _testObject.Handle (givenException, givenResponse);

            givenResponse.Messages.Should ().HaveCount (givenMessages.Count ());
            givenResponse.Messages.Select (m => m.MessageText).Should ().BeEquivalentTo (givenMessages.Select(x=>x.MessageText));
        }
    }
}