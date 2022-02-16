using CVU.ERP.Common.Data.Entities;
using CVU.ERP.Common.Extensions;
using CVU.ERP.StorageService.Context;
using FluentValidation;
using FluentValidation.Validators;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace CVU.ERP.Module.Application.StorageFileServices.Validators
{
    public class StorageFileMustExistValidator<TEntity> : AbstractValidator<string> where TEntity : SoftDeleteBaseEntity
    {
        private readonly StorageDbContext _storageDbContext;

        public StorageFileMustExistValidator(StorageDbContext storageDbContext, string errorCode, string errorMessage)
        {
            _storageDbContext = storageDbContext;

            RuleFor(x => x).Custom((id, c) => ExistentRecord(id, errorCode, errorMessage, c));
        }

        private void ExistentRecord(string id, string errorCode, string errorMessage, CustomContext context)
        {
            DbSet<TEntity> collections = _storageDbContext.Set<TEntity>();

            var existent = collections.Any(x => x.Id.ToString() == id);

            if (!existent)
            {
                context.AddFail(errorCode, errorMessage);
            }
        }
    }
}
