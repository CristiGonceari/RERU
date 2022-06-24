using CODWER.RERU.Core.Application.Validation;
using CVU.ERP.Common.Data.Persistence.EntityFramework.Validators;
using CVU.ERP.Common.Validation;
using FluentValidation;
using RERU.Data.Entities;
using RERU.Data.Persistence.Context;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.KinshipRelations.RemoveKinshipRelation
{
    public class RemoveKinshipRelationCommandValidator : AbstractValidator<RemoveKinshipRelationCommand>
    {
        public RemoveKinshipRelationCommandValidator(AppDbContext appDbContext)
        {
            RuleFor(x => x.Id)
                .SetValidator(new ItemMustExistValidator<KinshipRelation>(appDbContext, ValidationCodes.KINSHIP_RELATION_NOT_FOUND, ValidationMessages.NotFound));
        }
    }
}
