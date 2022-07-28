using System;
using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;

namespace CVU.ERP.ServiceProvider.Exceptions
{
    public class CoreClientResponseNotSuccessfulException : Exception
    {
        public CoreClientResponseNotSuccessfulException(IEnumerable<Message> messages)
        {
            Messages = messages;
        }

        public IEnumerable<Message> Messages { set; get; }
    }
}