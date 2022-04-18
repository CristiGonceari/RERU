using System.Collections.Generic;
using CVU.ERP.Common.Data.Entities;
using RERU.Data.Entities.Enums;

namespace RERU.Data.Entities
{
    public class Module : SoftDeleteBaseEntity 
    {
        public Module () {
            Roles = new List<ModuleRole> ();
            Permissions = new List<ModulePermission> ();
        }

        public string Code { set; get; }
        public string Color { set; get; }
        public string Name { set; get; }
        public int Priority { set; get; }
        
        public ModuleTypeEnum Type { set; get; }

        public string PublicUrl { set; get; }
        public string Icon { set; get; }

        public string InternalGatewayAPIPath { set; get; }

        public ModuleStatus Status { set; get; }

        public List<ModuleRole> Roles { set; get; }
        public List<ModulePermission> Permissions { set; get; }
    }
}