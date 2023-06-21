using CODWER.RERU.Identity.Web.Quickstart.Enums;

namespace CODWER.RERU.Identity.Web.Quickstart.Account
{
    public class MPassErrorRedirectViewModel
    {
        public LoggingErrorTypeEnum LoggingErrorTypeEnum { get; set; }
        public string RedirectLoginUri { get; set; }
        public string RedirectMPassUri { get; set; }
    }
}
