using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Entities.PersonalEntities.Enums;
using RERU.Data.Persistence.Context;
using CVU.ERP.Logging;
using CVU.ERP.Logging.Models;
using CVU.ERP.Module.Application.Models.Internal;
using CVU.ERP.ServiceProvider.Clients;
using CVU.ERP.ServiceProvider.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;
using RERU.Data.Entities;

namespace CODWER.RERU.Personal.Application.UserProfiles.CreateUserForContractor
{
    public class CreateUserForContractorCommandHandler : IRequestHandler<CreateUserForContractorCommand, int>
    {
        private readonly ICoreClient _coreClient;
        private readonly AppDbContext _appDbContext;
        private ILoggerService<CreateUserForContractorCommandHandler> _loggerService;

        public CreateUserForContractorCommandHandler(ICoreClient coreClient,
            AppDbContext appDbContext,
            ILoggerService<CreateUserForContractorCommandHandler> loggerService)
        {
            _coreClient = coreClient;
            _appDbContext = appDbContext;
            _loggerService = loggerService;
        }

        public async Task<int> Handle(CreateUserForContractorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentContractor = await _appDbContext.Contractors
                    .Include(x=>x.UserProfile)
                    .ThenInclude(x => x.Bulletin)
                    .Include(x => x.Permission)
                    .Include(x => x.UserProfile)
                    .FirstAsync(x => x.Id == request.ContractorId);

                await AddContractorContact(request.ContractorId, request.Email);
                
                currentContractor.Permission ??= new ContractorLocalPermission
                {
                    GetGeneralData = true
                };

                var skipGetApplicationUser = currentContractor.UserProfile == null;

                currentContractor.UserProfile ??= new UserProfile();

                var userApplication = await GetOrCreateCoreUser(currentContractor, request, skipGetApplicationUser);

                return await AssignCoreUserToUserProfile(currentContractor.UserProfile, userApplication);
            }
            catch (Exception e)
            {
                throw new Exception($"----ERROR CreateUserForContractorCommand {e.Message}");
            }
        }

        private async Task<int> AssignCoreUserToUserProfile(UserProfile userProfile, ApplicationUser coreUser)
        {
            await AddContractorContact((int)userProfile.Contractor.Id, coreUser.Email);

            userProfile.Id = int.Parse(coreUser.Id);
            userProfile.Email = coreUser.Email;
            userProfile.Idnp = coreUser.Idnp;

            await _appDbContext.SaveChangesAsync();

            await LogAction(userProfile.Id);

            return userProfile.Id;
        }

        private async Task<ApplicationUser> GetOrCreateCoreUser(Contractor currentContractor, CreateUserForContractorCommand request, bool skipGetApplicationUser)
        {
            //return !skipGetApplicationUser && await _coreClient.ExistUserInCore(currentContractor.UserProfile.Id.ToString())
            //        ? await _coreClient.GetApplicationUser(currentContractor.UserProfile.ToString())
            //        : await CreateUserInCore(currentContractor, request);
            return new ApplicationUser();
        }

        private async Task<ApplicationUser> CreateUserInCore(Contractor currentContractor, CreateUserForContractorCommand request)
        {
            //ApplicationUser userApplication;
            ApplicationUser userApplication = new ApplicationUser();
            var newUser = new InternalUserProfileCreate
            {
                Name = currentContractor.FirstName,
                LastName = currentContractor.LastName,
                FatherName = currentContractor.FatherName,
                Idnp = currentContractor.UserProfile.Idnp,
                Email = request.Email,
                NotifyAccountCreated = true,
                ModuleRoles = request.ModuleRoles
            };

            try
            {
                //userApplication = await _coreClient.CreateUserProfile(newUser);
            }
            catch (Exception e)
            {
                throw new Exception($"CoreClient ERROR: {e.Message} user wasn't created.");
            }

            if (userApplication == null)
            {
                throw new Exception("User Application is null on create Core-User");
            }

            return userApplication;
        }

        private async Task AddContractorContact(int contractorId, string email)
        {
            if (!_appDbContext.Contacts.Any(c=> c.ContractorId == contractorId && c.Value == email && c.Type == ContactTypeEnum.Email))
            {
                var newContact = new Contact
                {
                    ContractorId = contractorId,
                    Value = email,
                    Type = ContactTypeEnum.Email
                };

                await _appDbContext.Contacts.AddAsync(newContact);
            }
        }

        private async Task LogAction(int userProfileId)
        {
            var userProfile = await _appDbContext.UserProfiles
                .Include(x => x.Contractor)
                .FirstAsync(x => x.Id == userProfileId);

            await _loggerService.Log(LogData.AsPersonal($"UserProfile {userProfile.Contractor.FirstName} {userProfile.Contractor.LastName} was created/updated", userProfile)); ; ;
        }
    }
}