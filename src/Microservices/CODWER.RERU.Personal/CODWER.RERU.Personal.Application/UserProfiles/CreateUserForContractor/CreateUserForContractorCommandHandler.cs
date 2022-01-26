using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Entities;
using CODWER.RERU.Personal.Data.Entities.Enums;
using CODWER.RERU.Personal.Data.Entities.User;
using CODWER.RERU.Personal.Data.Persistence.Context;
using CVU.ERP.Module.Application.Clients;
using CVU.ERP.Module.Application.Models;
using CVU.ERP.Module.Application.Models.Internal;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CODWER.RERU.Personal.Application.UserProfiles.CreateUserForContractor
{
    public class CreateUserForContractorCommandHandler : IRequestHandler<CreateUserForContractorCommand, int>
    {
        private readonly ICoreClient _coreClient;
        private readonly AppDbContext _appDbContext;

        public CreateUserForContractorCommandHandler(ICoreClient coreClient, AppDbContext appDbContext)
        {
            _coreClient = coreClient;
            _appDbContext = appDbContext;
        }

        public async Task<int> Handle(CreateUserForContractorCommand request, CancellationToken cancellationToken)
        {
            try
            {
                var currentContractor = await _appDbContext.Contractors
                    .Include(x => x.Permission)
                    .Include(x => x.UserProfile)
                    .FirstAsync(x => x.Id == request.ContractorId);

                await AddContractorContact(request.ContractorId, request.Email);
                
                currentContractor.Permission ??= new ContractorLocalPermission
                {
                    GetGeneralData = true
                };

                var skipGetApplicationUser = currentContractor.UserProfile == null;

                currentContractor.UserProfile ??= new UserProfile
                {
                    ContractorId = currentContractor.Id
                };

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
            await AddContractorContact((int)userProfile.ContractorId, coreUser.Email);

            userProfile.Email = coreUser.Email;
            userProfile.UserId = coreUser.Id;

            await _appDbContext.SaveChangesAsync();

            return userProfile.Id;
        }

        private async Task<ApplicationUser> GetOrCreateCoreUser(Contractor currentContractor, CreateUserForContractorCommand request, bool skipGetApplicationUser)
        {
            return !skipGetApplicationUser && await _coreClient.ExistUserInCore(currentContractor.UserProfile.UserId)
                    ? await _coreClient.GetApplicationUser(currentContractor.UserProfile.UserId)
                    : await CreateUserInCore(currentContractor, request);
        }

        private async Task<ApplicationUser> CreateUserInCore(Contractor currentContractor, CreateUserForContractorCommand request)
        {
            ApplicationUser userApplication;
            var newUser = new InternalUserProfileCreate
            {
                Name = currentContractor.FirstName,
                LastName = currentContractor.LastName,
                //FatherName = currentContractor.FatherName,
                //Idnp = currentContractor.Bulletin.Idnp,
                Email = request.Email,
                NotifyAccountCreated = true,
                ModuleRoles = request.ModuleRoles
            };

            try
            {
                userApplication = await _coreClient.CreateUserProfile(newUser);
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
    }
}