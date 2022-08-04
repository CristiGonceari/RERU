using System.Linq;
using CODWER.RERU.Personal.Application.FieldTypes.Validators;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureRecords.RecordValues;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

namespace CODWER.RERU.Personal.Application.Validators.NomenclatureRecordValue
{
    public class NomenclatureRecordValueValidator : AbstractValidator<NomenclatureRecordDto>
    {
        private readonly AppDbContext _appDbContext;

        public NomenclatureRecordValueValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.Name).NotEmpty()
                .WithMessage(ValidationMessages.InvalidInput)
                .WithErrorCode(ValidationCodes.INVALID_NAME);

            RuleFor(x => x.NomenclatureTypeId)
                .SetValidator(new ItemMustExistValidator<NomenclatureType>(appDbContext, ValidationCodes.NOMENCLATURE_TYPE_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x).Custom(ValidateNomenclatureValueColumns);
        }

        private void ValidateNomenclatureValueColumns(NomenclatureRecordDto recordDto, CustomContext context)
        {
            //validate nomenclatureRecord
            var distinctRecordValues = recordDto.RecordValues.Select(x => x.NomenclatureRecordId).Distinct().ToList();

            if (!distinctRecordValues.Any())
            {
                //empty record values;
                return;
            }

            if (recordDto.Id != distinctRecordValues.First()  || distinctRecordValues.Count > 1)
            {
                context.AddFail(ValidationCodes.INVALID_INPUT);
            }

            var dtoNomenclatureRecordColumns = recordDto.RecordValues.Select(rv => rv.NomenclatureColumnId).ToList();

            var dbNomenclatureRecordColumns = _appDbContext.NomenclatureColumns
                .Where(nc => nc.NomenclatureTypeId == recordDto.NomenclatureTypeId)
                .ToList();

            if (dtoNomenclatureRecordColumns.Distinct().Count() != dbNomenclatureRecordColumns.Select(x => x.Id).Distinct().Count())
            {
                context.AddFail(ValidationCodes.INVALID_COLUMN_DATA, ValidationMessages.InvalidReference);
                return;
            }

            if (!dtoNomenclatureRecordColumns.All(dt => dbNomenclatureRecordColumns.Select(x => x.Id).Contains(dt)))
            {
                context.AddFail(ValidationCodes.NOMENCLATURE_TYPE_NOT_FOUND, ValidationMessages.InvalidReference);
                return;
            }

            foreach (var dbNomenclatureRecordColumn in dbNomenclatureRecordColumns)
            {
                var recordValue = recordDto.RecordValues.FirstOrDefault(x => x.NomenclatureColumnId == dbNomenclatureRecordColumn.Id);

                if (recordValue == null)
                {
                    context.AddFail(ValidationCodes.NOMENCLATURE_RECORD_VALUE_NOT_FOUND, ValidationMessages.InvalidReference);
                    return;
                }

                ValidateRecordValue(dbNomenclatureRecordColumn, recordValue, context);
            }
        }

        private void ValidateRecordValue(NomenclatureColumn dbNomenclatureRecordColumn, RecordValueDto recordValue, CustomContext context)
        {
            switch (dbNomenclatureRecordColumn.Type)
            {
                case FieldTypeEnum.Boolean:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new BooleanValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_BOOLEAN_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.Character:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new CharacterValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_CHARACTER_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.Date:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new DateTimeValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_DATE_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.DateTime:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new DateTimeValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_DATETIME_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.Double:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new DoubleValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_DOUBLE_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.Email:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new EmailValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_EMAIL_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.Integer:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new IntegerValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_INTEGER_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
                case FieldTypeEnum.Text:
                    {
                        if (dbNomenclatureRecordColumn.IsMandatory && string.IsNullOrEmpty(recordValue.StringValue)
                            || dbNomenclatureRecordColumn.IsMandatory && new TextValueValidator().Validate(recordValue.StringValue).Errors.Any())
                        {
                            context.AddFail(ValidationCodes.INVALID_TEXT_VALUE, ValidationMessages.InvalidInput);
                        }
                        break;
                    }
            }
        }

    }
}
