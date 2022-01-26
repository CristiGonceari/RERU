using System;
using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.Response.Messages;

namespace CVU.ERP.Module.Application.Exceptions
{
    public class EvaluationClientResponseNotSuccessfulException : Exception
    {
        public EvaluationClientResponseNotSuccessfulException(IEnumerable<Message> messages)
        {
            Messages = messages;
        }

        public IEnumerable<Message> Messages { set; get; }
    }
}
