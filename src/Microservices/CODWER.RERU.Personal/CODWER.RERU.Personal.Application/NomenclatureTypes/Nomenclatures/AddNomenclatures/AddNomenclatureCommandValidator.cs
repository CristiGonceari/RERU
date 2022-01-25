using CODWER.RERU.Personal.Application.Validation;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.NomenclatureTypes.Nomenclatures.AddNomenclatures
{
    public class AddNomenclatureCommandValidator : AbstractValidator<AddNomenclatureCommand>
    {
        public AddNomenclatureCommandValidator()
        {
            RuleFor(x => x.Data.Name).NotEmpty()
                .WithErrorCode(ValidationCodes.INVALID_INPUT)
                .WithMessage(ValidationMessages.InvalidInput);
        }
    }
}
