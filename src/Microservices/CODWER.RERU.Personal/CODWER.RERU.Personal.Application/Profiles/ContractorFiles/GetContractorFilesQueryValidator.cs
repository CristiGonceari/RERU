using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.Files;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Module.Common.MessageCodes;
using CVU.ERP.StorageService.Entities;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Profiles.ContractorFiles
{
    public class GetContractorFilesQueryValidator : AbstractValidator<GetContractorFilesQuery>
    {
        public GetContractorFilesQueryValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            RuleFor(x => (int)x.FileType)
                .SetValidator(new ExistInEnumValidator<FieldTypeEnum>());

            RuleFor(x => x.FileType)
                .Must(x => x != null)
                .WithErrorCode(ValidationCodes.INVALID_FILE_TYPE)
                .WithMessage(ValidationMessages.InvalidReference);

            var contractorId = userProfileService.GetCurrentContractorId().Result;

            var permission = appDbContext.ContractorPermissions
                .FirstOrDefault(x => x.ContractorId == contractorId);

            When(x => permission != null, () =>
            {

                When(x => x.FileType == FileTypeEnum.order, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataOrders)
                        .Must(x => x);
                });

                When(x => x.FileType == FileTypeEnum.cim, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataCim)
                        .Must(x => x);
                });

                When(x => x.FileType == FileTypeEnum.identityfiles, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataIdentity)
                        .Must(x => x);
                });

                When(x => x.FileType == FileTypeEnum.request, () =>
                {
                    RuleFor(x => permission.GetDocumentsDataRequest)
                        .Must(x => x);
                });
            });
        }
    }
}
