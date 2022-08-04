using RERU.Data.Entities.PersonalEntities.Enums;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.FieldTypes.Validators
{
    public class FieldTypeValidator : AbstractValidator<FieldTypeObject>
    {
        public FieldTypeValidator()
        {
          
            RuleFor(x => x.Value).SetValidator(new TextValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Text);

            RuleFor(x => x.Value).SetValidator(new CharacterValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Character);

            RuleFor(x => x.Value).SetValidator(new IntegerValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Integer);

            RuleFor(x => x.Value).SetValidator(new DoubleValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Double);

            RuleFor(x => x.Value).SetValidator(new BooleanValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Boolean);

            RuleFor(x => x.Value).SetValidator(new DateTimeValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Date || x.FieldType == FieldTypeEnum.DateTime);

            RuleFor(x => x.Value).SetValidator(new IntegerValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Integer);

            RuleFor(x => x.Value).SetValidator(new EmailValueValidator())
                .When(x => x.FieldType == FieldTypeEnum.Email);
        }
    }
}
