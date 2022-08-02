﻿using System;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators.EnumValidators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.Contractors;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities.Enums;
using RERU.Data.Entities;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.Bulletin;

namespace CODWER.RERU.Personal.Application.Contractors
{
    public class ContractorValidator : AbstractValidator<AddEditContractorDto>
    {
        public ContractorValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.FirstName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.LastName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.FatherName).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.BirthDate)
                .Must(x => x > DateTime.Now.AddYears(-100))
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => (int)x.Sex)
                .SetValidator(new ExistInEnumValidator<SexTypeEnum>());

            RuleFor(x => x.Idnp)
                .SetValidator(new IdnpValidator());

            //RuleFor(x => BaseNomenclatureTypesEnum.BloodTypes.NewRecordToValidate(x.BloodTypeId))
            //    .SetValidator(new RecordFromBaseNomenclatureTypesValidator(appDbContext));

            RuleFor(x => x.CandidateCitizenshipId)
                .SetValidator(x => new ItemMustExistValidator<CandidateCitizenship>(appDbContext, ValidationCodes.INVALID_ID,
                    ValidationMessages.InvalidReference));

            RuleFor(x => x.CandidateNationalityId)
               .SetValidator(x => new ItemMustExistValidator<CandidateNationality>(appDbContext, ValidationCodes.INVALID_ID,
                   ValidationMessages.InvalidReference));

            RuleFor(x => x.WorkPhone).NotEmpty()
               .WithMessage(ValidationMessages.InvalidInput)
               .WithErrorCode(ValidationCodes.INVALID_INPUT);

            RuleFor(x => x.HomePhone).NotEmpty()
               .WithMessage(ValidationMessages.InvalidInput)
               .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
