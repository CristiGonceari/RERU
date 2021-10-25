using System.Collections.Generic;
using CVU.ERP.Common.DataTransferObjects.SelectValues;

namespace CVU.ERP.Module.Application.Models
{
    public class ModuleRolesDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<SelectItem> Roles { get; set; }
    }
}
