﻿using System.Collections.Generic;
using AutoMapper;
using RERU.Data.Entities.PersonalEntities;

namespace CODWER.RERU.Personal.Application.Contractors.ContractorMappings
{
    public class ContractorSelectItemConverter : IValueConverter<Contractor, List<KeyValuePair<string,string>>>
    {
        public List<KeyValuePair<string, string>> Convert(Contractor sourceMember, ResolutionContext context)
        {
            return new List<KeyValuePair<string, string>>
            {
                new KeyValuePair<string, string>("department", new DepartmentNameConverter().Convert(sourceMember, context)),
                new KeyValuePair<string, string>("position", new RoleConverter().Convert(sourceMember,context))
            };
        }
    }
}
