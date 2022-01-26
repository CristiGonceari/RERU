using CVU.ERP.Module.Common.Models.ClientConfigurations;

namespace CVU.ERP.Module.Common.Models {
    public class ModuleConfiguration {
        public ModuleConfiguration () {
            Authentication = new ModuleAuthenticationConfiguration ();
            UsesAuthentication = true;
        }
        public bool UsesAuthentication { set; get; }
        public ModuleAuthenticationConfiguration Authentication { set; get; }
        public CoreClientConfiguration CoreClient { set; get; }
        public EvaluationClientConfiguration EvaluationClient { set; get; }
        public PersonalClientConfiguration PersonalClient { set; get; }
        public LoggingClientConfiguration LoggingClient { set; get; }
        public bool IsCore { set; get; }
        public string InternalGatewayBaseUrl { set; get; }
        public bool UseMockApplicationUser { set; get; }
    }
}