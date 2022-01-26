﻿using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFiles
{
    public class GetContractorFilesQueryValidator : AbstractValidator<GetContractorFilesQuery>
    {
        public GetContractorFilesQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.Type)
                .SetValidator(new ExistInEnumValidator<FieldTypeEnum>());

            RuleFor(x => x.Type)
                .Must(x => x != null)
                .WithErrorCode(ValidationCodes.INVALID_FILE_TYPE)
                .WithMessage(ValidationMessages.InvalidReference);

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            var permission = appDbContext.ContractorPermissions
                .FirstOrDefault(x => x.ContractorId == contractorId);

            When(x => permission != null, () =>
            {

                When(x => x.Type == FileTypeEnum.Order, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataOrders)
                        .Must(x => x);
                });

                When(x => x.Type == FileTypeEnum.Cim, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataCim)
                        .Must(x => x);
                });

                When(x => x.Type == FileTypeEnum.Identity, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataIdentity)
                        .Must(x => x);
                });

                When(x => x.Type == FileTypeEnum.Request, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataRequest)
                        .Must(x => x);
                });
            });
        }
    }
}