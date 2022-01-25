﻿using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.Bonuses;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.Bonuses.GetBonuses
{
    [ModuleOperation(permission: PermissionCodes.BONUSES_GENERAL_ACCESS)]

    public class GetBonusesQuery : PaginatedQueryParameter, IRequest<PaginatedModel<BonusDto>>
    {
        public int? ContractorId { get; set; }
    }
}
