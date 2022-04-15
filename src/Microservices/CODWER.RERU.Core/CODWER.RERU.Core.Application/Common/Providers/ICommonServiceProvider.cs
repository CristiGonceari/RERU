using AutoMapper;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Data.Persistence.Context;
using MediatR;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Core.Application.Common.Providers 
{
    public interface ICommonServiceProvider {
        AppDbContext AppDbContext { get; }
        UserManagementDbContext UserManagementDbContext { get; }
        IMapper Mapper { get; }
        IPaginationService PaginationService { get; }
        IMediator Mediator { get; }
    }
}