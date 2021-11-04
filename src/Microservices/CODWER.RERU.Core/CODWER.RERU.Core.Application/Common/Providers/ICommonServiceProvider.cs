using AutoMapper;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Core.Application.Common.Providers {
    public interface ICommonServiceProvider {
        CoreDbContext CoreDbContext { get; }
        UserManagementDbContext UserManagementDbContext { get; }
        IMapper Mapper { get; }
        IPaginationService PaginationService { get; }
        IMediator Mediator { get; }
    }
}