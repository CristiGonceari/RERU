﻿using CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorProfile;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorProfile.GetContractorAvatar
{
    class GetContractorAvatarQueryValidator : AbstractValidator<GetContractorProfileQuery>
    {
        public GetContractorAvatarQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(_ => new ContractorLocalPermission { GetGeneralData = true })
                .SetValidator(new LocalPermissionValidator(userProfileService, appDbContext));
        }
    }
}