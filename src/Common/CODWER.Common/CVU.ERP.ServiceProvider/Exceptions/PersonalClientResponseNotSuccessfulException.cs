using System;
using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;

namespace CVU.ERP.ServiceProvider.Exceptions
{
    public class PersonalClientResponseNotSuccessfulException : Exception
    {
        public PersonalClientResponseNotSuccessfulException(IEnumerable<Message> messages)
        {
            Messages = messages;
        }

        public IEnumerable<Message> Messages { set; get; }
    }
}
