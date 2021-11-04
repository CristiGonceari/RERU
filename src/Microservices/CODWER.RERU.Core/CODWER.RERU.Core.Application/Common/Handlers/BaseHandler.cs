using AutoMapper;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Application.Common.Providers;
using CODWER.RERU.Core.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Core.Application.Common.Handlers {
    public class BaseHandler {
        private readonly ICommonServiceProvider _commonServiceProvider;
        public BaseHandler (ICommonServiceProvider commonServiceProvider) {
            _commonServiceProvider = commonServiceProvider;
        }
        protected IMapper Mapper => _commonServiceProvider.Mapper;
        protected CoreDbContext CoreDbContext => _commonServiceProvider.CoreDbContext;
        protected UserManagementDbContext UserManagementDbContext => _commonServiceProvider.UserManagementDbContext;
        protected IPaginationService PaginationService => _commonServiceProvider.PaginationService;
        protected IMediator Mediator => _commonServiceProvider.Mediator;
    }
}