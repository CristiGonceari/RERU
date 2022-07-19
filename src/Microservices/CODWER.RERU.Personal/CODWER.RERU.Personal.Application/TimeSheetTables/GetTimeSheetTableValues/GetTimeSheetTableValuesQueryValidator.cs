using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Validation;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.GetTimeSheetTableValues
{
   public class GetTimeSheetTableValuesQueryValidator : AbstractValidator<GetTimeSheetTableValuesQuery>
    {
        public GetTimeSheetTableValuesQueryValidator(AppDbContext appDbContext)
        {
           RuleFor(x => x.FromDate).NotEmpty()
               .WithMessage(ValidationMessages.InvalidInput)
               .WithErrorCode(ValidationCodes.INVALID_INPUT); 

           RuleFor(x => x.ToDate).NotEmpty()
               .WithMessage(ValidationMessages.InvalidInput)
               .WithErrorCode(ValidationCodes.INVALID_INPUT);
        }
    }
}
