using CVU.ERP.MSignService.API.Controllers;
using Microsoft.AspNetCore.Mvc.ApplicationParts;
using Microsoft.Extensions.DependencyInjection;

namespace CVU.ERP.MSignService.API.DependencyInjection
{
    public static class ServicesSetup
    {
        public static IMvcBuilder AddMSignCommonModuleControllers(this IMvcBuilder builder)
        {
            builder.PartManager.ApplicationParts.Add(new AssemblyPart(typeof(MSignServiceController).Assembly));

            return builder;
        }
    }
}
