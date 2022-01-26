﻿using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFamilyMembers.GetContractorFamilyMembers
{
    public class GetContractorFamilyMembersQueryValidator : AbstractValidator<GetContractorFamilyMembersQuery>
    {
        public GetContractorFamilyMembersQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(_ => new ContractorLocalPermission { GetFamilyComponentData = true })
                .SetValidator(new LocalPermissionValidator(userProfileService, appDbContext));
        }
    }
}