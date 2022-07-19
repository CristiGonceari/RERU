using System.Linq;
using RERU.Data.Persistence.Context;
using CVU.ERP.Common.Data.Entities;
using CVU.ERP.Common.Extensions;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.Validators
{
    public class ItemMustExistValidator<TEntity> : AbstractValidator<int> where TEntity : SoftDeleteBaseEntity
    {
        private readonly AppDbContext _appDbContext;

        public ItemMustExistValidator(AppDbContext appDbContext, string errorCode, string errorMessage)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x).Custom((id, c) => ExistentRecord(id, errorCode, errorMessage, c));
        }

        private void ExistentRecord(int id, string errorCode, string errorMessage, CustomContext context)
        {
            DbSet<TEntity> collections = _appDbContext.Set<TEntity>();

            var existent = collections.Any(x => x.Id == id);

            if (!existent)
            {
                context.AddFail(errorCode, errorMessage);
            }
        }
    }
}
