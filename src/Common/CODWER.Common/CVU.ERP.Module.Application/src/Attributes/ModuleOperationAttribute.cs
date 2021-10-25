using System;

namespace CVU.ERP.Module.Application.Attributes {
    public class ModuleOperationAttribute : Attribute {
        public ModuleOperationAttribute (string permission = null, bool requiresAuthentication = false) {
            Permission = permission;
            RequiresAuthentication = requiresAuthentication;
        }
        public string Permission { private set; get; }
        public bool RequiresAuthentication { private set; get; }
    }
}