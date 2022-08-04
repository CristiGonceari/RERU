using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CVU.ERP.ServiceProvider.Models
{
    public class ModuleRolesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectItem> Roles { get; set; }
    }
}
