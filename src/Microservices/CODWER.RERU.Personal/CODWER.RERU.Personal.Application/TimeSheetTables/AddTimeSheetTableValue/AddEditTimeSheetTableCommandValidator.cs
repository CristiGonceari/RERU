using RERU.Data.Persistence.Context;
using FluentValidation;

namespace CODWER.RERU.Personal.Application.TimeSheetTables.AddTimeSheetTableValue
{
   public class AddEditTimeSheetTableCommandValidator : AbstractValidator<AddEditTimeSheetTableCommand>
   {
       public AddEditTimeSheetTableCommandValidator(AppDbContext appDbContext)
       {
           RuleFor(x => x.Data).SetValidator(new TimeSheetTableValidator(appDbContext));
       }
    }
}
