﻿using CODWER.RERU.Personal.Application.Permissions;
using CODWER.RERU.Personal.DataTransferObjects.OrganizationalCharts;
using CVU.ERP.Common.Pagination;
using CVU.ERP.Module.Application.Attributes;
using MediatR;

namespace CODWER.RERU.Personal.Application.OrganizationalCharts.GetOrganizationalCharts
{
    [ModuleOperation(permission: PermissionCodes.ORGANIZATIONAL_CHART_GENERAL_ACCESS)]

    public class GetOrganizationalChartsQuery : PaginatedQueryParameter, IRequest<PaginatedModel<OrganizationalChartDto>>
    {
    }
}