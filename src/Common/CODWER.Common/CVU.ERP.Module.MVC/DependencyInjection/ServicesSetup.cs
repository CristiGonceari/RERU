using CVU.ERP.Modules.MVC.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.Module.MVC.DependencyInjection
{
    public static class ModulesCommonMVCServicesSetup
    {
        public static IMvcBuilder AddCommonModuleControllers(this IMvcBuilder builder)
        {
            builder.PartManager.ApplicationParts.Add(new AssemblyPart (typeof (DiscoverController).Assembly));
            return builder;
        }
    }
}