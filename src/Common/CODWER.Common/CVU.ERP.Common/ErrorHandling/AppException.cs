using System;
using System.Net;

namespace CVU.ERP.Common.ErrorHandling
{
    public class AppException : Exception
    {
        public AppException(string message, HttpStatusCode httpStatusCode) : base(message)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        public AppException(string message, HttpStatusCode httpStatusCode, Exception innerException) : base(message, innerException)
        {
            this.HttpStatusCode = httpStatusCode;
        }

        public HttpStatusCode HttpStatusCode { get; set; }
    }
}
