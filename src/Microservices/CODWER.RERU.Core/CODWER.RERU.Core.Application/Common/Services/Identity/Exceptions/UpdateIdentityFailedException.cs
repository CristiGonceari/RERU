using System;
using System.Linq;

namespace CODWER.RERU.Core.Application.Common.Services.Identity.Exceptions
{
    public class UpdateIdentityFailedException : Exception
    {
        public UpdateIdentityFailedException()
        {
            Errors = new string[] { };
        }

        public UpdateIdentityFailedException(string message)
        {
            Errors = new string[] { };
            Errors.Append(message);
        }

        public UpdateIdentityFailedException(string[] errors)
        {
            Errors = errors;
        }

        public string[] Errors { set; get; }
    }
}
