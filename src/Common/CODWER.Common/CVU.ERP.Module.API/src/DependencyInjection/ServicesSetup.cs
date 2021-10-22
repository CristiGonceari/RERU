using CVU.ERP.Module.API.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.Module.API.DependencyInjection {
    public static class ModulesCommonMVCServicesSetup {
        public static IMvcBuilder AddCommonModuleControllers (this IMvcBuilder builder) {
            builder.PartManager.ApplicationParts.Add (new AssemblyPart (typeof (DiscoverController).Assembly));
            return builder;
        }
    }
}