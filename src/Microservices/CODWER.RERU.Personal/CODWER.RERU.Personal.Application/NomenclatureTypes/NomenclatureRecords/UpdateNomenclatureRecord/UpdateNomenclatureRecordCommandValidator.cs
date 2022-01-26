﻿using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using CODWER.RERU.Personal.Application.Validators;
using CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue;
using CODWER.RERU.Personal.Data.Entities.NomenclatureType.NomenclatureRecords;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.NomenclatureRecords.UpdateNomenclatureRecord
{
    public class UpdateNomenclatureRecordCommandValidator : AbstractValidator<UpdateNomenclatureRecordCommand>
    {
        public UpdateNomenclatureRecordCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Data)
                .SetValidator(new ValidUpdateNomenclatureRecordValidator(appDbContext));

            RuleForEach(x => x.Data.RecordValues.Select(rv => rv.NomenclatureRecordId).ToList())
                .SetValidator(new ItemMustExistValidator<NomenclatureRecord>(appDbContext, ValidationCodes.NOMENCLATURE_RECORD_VALUE_NOT_FOUND, ValidationMessages.InvalidReference))
                .OverridePropertyName($"NomenclatureRecordValue");
        }
    }
}