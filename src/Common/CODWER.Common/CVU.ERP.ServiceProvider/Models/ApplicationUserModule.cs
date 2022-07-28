using System.Collections.Generic;

namespace CVU.ERP.ServiceProvider.Models 
{
    public class ApplicationUserModule 
    {
        public ApplicationUserModule()
        {
            Permissions = new List<string>();
        }

        public ApplicationModule Module { set; get; }
        public string Role { set; get; }
        public IEnumerable<string> Permissions { set; get; }
    }
}