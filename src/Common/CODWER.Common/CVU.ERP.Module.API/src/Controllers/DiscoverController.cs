using System.Reflection;
using CVU.ERP.Common.DataTransferObjects.Module;
using Microsoft.AspNetCore.Mvc;

namespace CVU.ERP.Module.API.Controllers {
    [ApiController]
    [Route ("api/[controller]")]
    public class DiscoverController : Controller {
        [HttpGet]
        public ModuleInfo Get () {
            return new ModuleInfo {
                Name = Assembly.GetEntryAssembly ().GetName ().FullName,
                    Version = Assembly.GetEntryAssembly ().GetName ().Version.ToString ()
            };
        }
    }
}