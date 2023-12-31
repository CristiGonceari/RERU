﻿using CVU.ERP.Module.Common.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using CVU.ERP.Module.Common.Models;

namespace CODWER.RERU.Evaluation.Application.Permissions
{
    public class ModulePermissionProvider : IModulePermissionProvider
    {
        public Task<ModulePermission[]> Get()
        {
            var permissions = new List<ModulePermission>();

            foreach (FieldInfo field in typeof(PermissionCodes).GetFields())
            {
                permissions.Add(new ModulePermission
                {
                    Code = field.GetRawConstantValue().ToString(),
                    Description = field.Name.Replace("_", " ")
                });
            }

            return Task.FromResult(permissions
                .OrderBy(x => x.Code)
                .ToArray());
        }
    }
}
