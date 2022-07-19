using System.Collections.Generic;
using System.Linq;
using CODWER.RERU.Personal.Application.Validation;
using RERU.Data.Persistence.Context;
using CODWER.RERU.Personal.DataTransferObjects.NomenclatureTypes.NomenclatureColumns;
using CVU.ERP.Common.Extensions;
using CVU.ERP.Common.Validation;
using FluentValidation;
using FluentValidation.Validators;
using RERU.Data.Entities.PersonalEntities.NomenclatureType;

namespace CODWER.RERU.Personal.Application.Validators.NomenclatureTableHeader
{
    public class ValidNomenclatureHeaderValidator : AbstractValidator<NomenclatureTableHeaderDto>
    {
        private readonly AppDbContext _appDbContext;

        public ValidNomenclatureHeaderValidator(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;

            RuleFor(x => x.NomenclatureTypeId)
                .SetValidator(new ItemMustExistValidator<NomenclatureType>(appDbContext, ValidationCodes.NOMENCLATURE_TYPE_NOT_FOUND, ValidationMessages.InvalidReference));

            RuleFor(x => x.NomenclatureColumns).Custom(ValidateColumnOrders);

            RuleFor(x => x.NomenclatureColumns.Where(nc=>nc.Id == 0).ToList()).Custom(ValidateColumnsToAdd); // id == 0 ? add : update

            RuleForEach(x => x.NomenclatureColumns.Where(nc => nc.Id != 0).Select(x => x.Id).ToList())
                .SetValidator(new ItemMustExistValidator<NomenclatureColumn>(appDbContext, ValidationCodes.NOMENCLATURE_COLUMN_NOT_FOUND, ValidationMessages.NotFound));
        }

        private void ValidateColumnOrders(List<NomenclatureColumnItemDto> columns, CustomContext context)
        {
            var orderList = columns.Select(x => x.Order).ToList();

            if (orderList.Any(x => x < 1 || x > orderList.Count)
                || orderList.Distinct().Count() != orderList.Count)
            {
                context.AddFail(ValidationCodes.INVALID_COLUMN_ORDER, ValidationMessages.InvalidInput);
            }
        }

        private void ValidateColumnsToAdd(List<NomenclatureColumnItemDto> columns, CustomContext context)
        {
            foreach (var nomenclatureColumnItemDto in columns)
            {
                if (string.IsNullOrEmpty(nomenclatureColumnItemDto.Name))
                {
                    context.AddFail(ValidationCodes.INVALID_NAME, ValidationMessages.InvalidInput);
                }

                if (nomenclatureColumnItemDto.Type == 0)
                {
                    context.AddFail(ValidationCodes.INVALID_COLUMN_TYPE, ValidationMessages.InvalidInput);
                }
            }
        }
    }
}
