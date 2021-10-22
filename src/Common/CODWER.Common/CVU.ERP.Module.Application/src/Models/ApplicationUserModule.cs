using System.Collections.Generic;

namespace CVU.ERP.Module.Application.Models {
    public class ApplicationUserModule {
        public ApplicationModule Module { set; get; }
        public string Role { set; get; }
        public IEnumerable<string> Permissions { set; get; }
    }
}