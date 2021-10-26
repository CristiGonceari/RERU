using System;
using System.Linq;

namespace CODWER.RERU.Core.Application.Common.Services.Identity.Exceptions
{
    public class CreateIdentityFailedException : Exception
    {
        public CreateIdentityFailedException()
        {
            Errors = new string[] { };
        }


        public CreateIdentityFailedException(string message)
        {
            Errors = new string[] { };
            Errors.Append(message);
        }

        public CreateIdentityFailedException(string[] errors)
        {
            Errors = errors;
        }

        public string[] Errors { set; get; }
    }
}