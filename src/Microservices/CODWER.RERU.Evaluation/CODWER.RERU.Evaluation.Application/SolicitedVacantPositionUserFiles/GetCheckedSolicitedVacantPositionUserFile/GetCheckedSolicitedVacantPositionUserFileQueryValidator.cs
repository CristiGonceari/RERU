using CODWER.RERU.Evaluation.Application.Validation;
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

namespace CODWER.RERU.Evaluation.Application.SolicitedVacantPositionUserFiles.GetCheckedSolicitedVacantPositionUserFile
{
    public class GetCheckedSolicitedVacantPositionUserFileQueryValidator : AbstractValidator<GetCheckedSolicitedVacantPositionUserFileQuery>
    {
        public GetCheckedSolicitedVacantPositionUserFileQueryValidator(AppDbContext appDbContext) 
        {
            RuleFor(x => x.Id)
                .SetValidator(x => new ItemMustExistValidator<SolicitedVacantPosition>(appDbContext, ValidationCodes.INVALID_SOLICITED_POSITION,
                    ValidationMessages.InvalidReference));
        }
    }
}
