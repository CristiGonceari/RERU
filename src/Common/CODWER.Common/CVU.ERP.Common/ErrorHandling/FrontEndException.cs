using System;
using System.Net;

namespace CVU.ERP.Common.ErrorHandling
{
    public class FrontEndException : AppException
    {
        public FrontEndException(string message) : base(message, HttpStatusCode.OK)
        {
        }

        public FrontEndException(string message, HttpStatusCode httpStatusCode, Exception innerException) : base(message, HttpStatusCode.OK, innerException)
        {
            this.HttpStatusCode = httpStatusCode;
        }
    }
}
