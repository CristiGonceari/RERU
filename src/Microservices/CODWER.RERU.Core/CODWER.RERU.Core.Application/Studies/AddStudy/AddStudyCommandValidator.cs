using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.ServiceProvider;
using FluentValidation;
using RERU.Data.Persistence.Context;
using System;

namespace CODWER.RERU.Core.Application.Studies.AddStudy
{
    public class AddStudyCommandValidator : AbstractValidator<AddStudyCommand>
    {
        public AddStudyCommandValidator(AppDbContext appDbContext, ICurrentApplicationUserProvider currentUserProvider)
        {
            RuleFor(x => x.Data).SetValidator(new StudyValidator(appDbContext, currentUserProvider));
        }
    }
}
