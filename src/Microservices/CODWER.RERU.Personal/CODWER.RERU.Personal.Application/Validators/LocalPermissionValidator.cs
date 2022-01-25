using System.Linq;
using CODWER.RERU.Personal.Application.Services;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Module.Common.MessageCodes;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.Validators
{
    public class LocalPermissionValidator : AbstractValidator<ContractorLocalPermission>
    {
        public LocalPermissionValidator(IUserProfileService userProfileService, AppDbContext appDbContext)
        {
            var contractorId = userProfileService.GetCurrentContractorId().Result;

            var permission = appDbContext.ContractorPermissions
                .FirstOrDefault(x => x.ContractorId == contractorId);

            RuleFor(_ => permission)
                .Must(x => x != null)
                .WithMessage(ValidationMessages.NotFound)
                .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);

            When(_ => permission != null, () =>
            {
                When(x => x.GetGeneralData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetGeneralData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetBulletinData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetBulletinData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetStudiesData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetStudiesData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetCimData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetCimData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetPositionsData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetPositionsData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetRanksData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetRanksData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetFamilyComponentData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetFamilyComponentData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetDocumentsDataIdentity, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetDocumentsDataIdentity)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetDocumentsDataOrders, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetDocumentsDataOrders)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetDocumentsDataCim, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetDocumentsDataCim)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetDocumentsDataRequest, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetDocumentsDataRequest)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetDocumentsDataVacation, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetDocumentsDataVacation)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });

                When(x => x.GetTimeSheetTableData, () =>
                {
                    RuleFor(_ => _)
                        .Must(x => permission.GetTimeSheetTableData)
                        .WithMessage(ValidationMessages.NotFound)
                        .WithErrorCode(ValidationCodes.LOCAL_PERMISSION_NOT_FOUND);
                });
            });
        }
    }
}
