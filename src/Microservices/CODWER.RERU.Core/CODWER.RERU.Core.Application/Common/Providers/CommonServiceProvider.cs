using AutoMapper;
using CVU.ERP.Common.Pagination;
using CODWER.RERU.Core.Data.Persistence.Context;
using MediatR;

namespace CODWER.RERU.Core.Application.Common.Providers {
    public class CommonServiceProvider : ICommonServiceProvider {
        public CommonServiceProvider (
            CoreDbContext coreDbContext,
            UserManagementDbContext usersDbContext,
            IMapper mapper,
            IPaginationService paginationService,
            IMediator mediator) {
            CoreDbContext = coreDbContext;
            Mapper = mapper;
            UserManagementDbContext = usersDbContext;
            PaginationService = paginationService;
            Mediator = mediator;
        }
        public CoreDbContext CoreDbContext { private set; get; }
        public IMapper Mapper { private set; get; }

        public UserManagementDbContext UserManagementDbContext { private set; get; }

        public IPaginationService PaginationService { private set; get; }

        public IMediator Mediator { private set; get; }
    }
}