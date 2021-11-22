using CODWER.RERU.Core.Application.Common.Handlers;
using CODWER.RERU.Core.Application.Common.Providers;
using CVU.ERP.Identity.Models;
using CVU.ERP.Module.Application.Providers;
using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Users.ChangeMyPassword
{
    public class ChangeMyPasswordCommandHandler : BaseHandler, IRequestHandler<ChangeMyPasswordCommand, Unit>
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly ICurrentApplicationUserProvider _userProvider;

        public ChangeMyPasswordCommandHandler(
            ICommonServiceProvider commonServicepProvider, ICurrentApplicationUserProvider userProvider,
            UserManager<ERPIdentityUser> userManager) : base(commonServicepProvider)
        {
            _userManager = userManager;
            _userProvider = userProvider;
        }

        public async Task<Unit> Handle(ChangeMyPasswordCommand request, CancellationToken cancellationToken)
        {
            var currentUser = await _userProvider.Get();
            var user = await UserManagementDbContext.Users.FirstOrDefaultAsync(u => u.Email == currentUser.Email);
           
            if (await _userManager.CheckPasswordAsync(user, request.oldPassword))
            {
                if(request.newPassword == request.repeatPassword)
                {
                    List<string> passwordErrors = new List<string>();

                    var validators = _userManager.PasswordValidators;

                    foreach(var validator in validators)
                    {
                        var result = await validator.ValidateAsync(_userManager, null, request.newPassword);

                        if (!result.Succeeded)
                        {
                            foreach (var error in result.Errors)
                            {
                                passwordErrors.Add(error.Description);
                                throw new Exception("Validation Error");
                            }
                        } else
                        {
                            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, request.newPassword);
                            await _userManager.UpdateAsync(user);
                        }                       
                    }  
                }   else 
                    throw new Exception("New password and Repeat password aren't the same");
            }
            else
            {
                throw new Exception("Wrong Old Password");
            }
            return Unit.Value;
        }
    }
}
