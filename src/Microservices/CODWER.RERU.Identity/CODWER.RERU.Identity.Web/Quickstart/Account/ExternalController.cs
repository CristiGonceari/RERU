using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Security.Claims;
using System.Threading.Tasks;
using Age.Integrations.MPass.Saml;
using AutoMapper;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Identity.Context;
using CVU.ERP.Identity.Models;
using CVU.ERP.Notifications.Email;
using CVU.ERP.Notifications.Services;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Events;
using IdentityServer4.Services;
using IdentityServer4.Stores;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using RERU.Data.Entities;
using RERU.Data.Entities.PersonalEntities;
using RERU.Data.Persistence.Context;

namespace CODWER.RERU.Identity.Web.Quickstart.Account
{

    [SecurityHeaders]
    [AllowAnonymous]
    public class ExternalController : Controller
    {
        private readonly UserManager<ERPIdentityUser> _userManager;
        private readonly SignInManager<ERPIdentityUser> _signInManager;
        private readonly IIdentityServerInteractionService _interaction;
        private readonly IClientStore _clientStore;
        private readonly IEventService _events;
        private readonly ILogger<ExternalController> _logger;
        private readonly IdentityDbContext _identityDbContext;
        private readonly AppDbContext _appDbContext;
        private readonly IMapper _mapper;
        private readonly INotificationService _notificationService;

        private IConfiguration Configuration { get; }

        public ExternalController(
            UserManager<ERPIdentityUser> userManager,
            SignInManager<ERPIdentityUser> signInManager,
            IIdentityServerInteractionService interaction,
            IClientStore clientStore,
            IEventService events,
            ILogger<ExternalController> logger,
            IdentityDbContext identityDbContext,
            IConfiguration configuration,
            AppDbContext appDbContext,
            IMapper mapper,
            INotificationService notificationService
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _interaction = interaction;
            _clientStore = clientStore;
            _events = events;
            _logger = logger;
            _identityDbContext = identityDbContext;
            Configuration = configuration;
            _appDbContext = appDbContext;
            _mapper = mapper;
            _notificationService = notificationService;
        }

        /// <summary>
        /// initiate roundtrip to external authentication provider
        /// </summary>
        [HttpGet]
        public IActionResult Challenge(string scheme, string returnUrl)
        {
            if (string.IsNullOrEmpty(returnUrl)) returnUrl = "~/";

            // validate returnUrl - either it is a valid OIDC URL or back to a local page
            if (Url.IsLocalUrl(returnUrl) == false && _interaction.IsValidReturnUrl(returnUrl) == false)
            {
                // user might have clicked on a malicious link - should be logged
                throw new Exception("invalid return URL");
            }

            // start challenge and roundtrip the return URL and scheme 
            AuthenticationProperties props;

            if (scheme == MPassSamlDefaults.AuthenticationScheme)
            {
                props = new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(CallbackMPass), new { returnDefaultUrl = returnUrl}),
                    Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", scheme },
                }
                };
            }
            else
            {
                props = new AuthenticationProperties
                {
                    RedirectUri = Url.Action(nameof(Callback)),
                    Items =
                {
                    { "returnUrl", returnUrl },
                    { "scheme", scheme },
                }
                };
            }
            

            return Challenge(props, scheme);
            
        }

        [HttpGet]
        public async Task<IActionResult> CallbackMPass(string returnDefaultUrl)
        {

            var result = await HttpContext.AuthenticateAsync(MPassSamlDefaults.AuthenticationScheme);

            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            if (result.Principal.FindFirst(MPassClaimTypes.EmailAdress) == null)
                //if (result.Principal.FindFirst(MPassClaimTypes.EmailAdress) == null || result.Principal.FindFirst(MPassClaimTypes.PhoneNumber) == null)
            {

                var vm = new MPassErrorRedirectViewModel() 
                { 
                    RedirectLoginUri = Configuration.GetValue<string>("MPassSaml:ServiceRootUrl"),
                    RedirectMPassUri = Configuration.GetValue<string>("MPassSaml:SamlLoginDestination")
                };
                return View(vm);

            }

            // lookup our user and external provider info
            var (identityUser, provider, claims, emailAdress) = await FindUserFromMPassProviderAsync(result);
            if (identityUser == null)
            {
                // this might be where you might initiate a custom workflow for user registration
                // in this sample we don't show how that would be done, as our sample implementation
                // simply auto-provisions new external user
                identityUser = await AutoProvisionMPassUserAsync(provider, claims);
            }
            else
            {
                // find userProfile
                var userProfile = await _appDbContext.UserProfiles
                        .Include(up => up.Identities.Where(i => i.Type == MPassSamlDefaults.AuthenticationScheme))
                        .FirstOrDefaultAsync(up => up.Email == emailAdress);

                if (userProfile.Identities.Count() == 0)
                {
                    var newIdentityUserProfile = new UserProfileIdentity()
                    {
                        UserProfileId = userProfile.Id,
                        Type = provider,
                        Identificator = identityUser.Id
                    };

                    userProfile.Identities.Add(newIdentityUserProfile);
                    await _appDbContext.SaveChangesAsync();
                }
            }

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps, MPassClaimTypes.SessionIndex);

            var principal = await _signInManager.CreateUserPrincipalAsync(identityUser);
            additionalLocalClaims.AddRange(principal.Claims);
            var name = principal.FindFirst(JwtClaimTypes.Name)?.Value ?? identityUser.Id;


            var isuser = new IdentityServerUser(identityUser.Id)
            {
                DisplayName = name,
                IdentityProvider = provider,
                AdditionalClaims = additionalLocalClaims
            };

            await HttpContext.SignInAsync(isuser, localSignInProps);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // retrieve return URL
            //var returnUrl = returnDefaultUrl ?? "~/";

            // check if external login is in the context of an OIDC request
            var context = await _interaction.GetAuthorizationContextAsync(returnDefaultUrl);
            //await _events.RaiseAsync(new UserLoginSuccessEvent(user.UserName, user.Id, user.UserName, clientId: context?.Client.ClientId));
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, identityUser.Id, name, true, context?.Client.ClientId));

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnDefaultUrl);
                }
            }

            return Redirect(returnDefaultUrl);
        }

        /// <summary>
        /// Post processing of external authentication
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> Callback()
        {
            // read external identity from the temporary cookie
            var result = await HttpContext.AuthenticateAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            if (result?.Succeeded != true)
            {
                throw new Exception("External authentication error");
            }

            if (_logger.IsEnabled(LogLevel.Debug))
            {
                var externalClaims = result.Principal.Claims.Select(c => $"{c.Type}: {c.Value}");
                _logger.LogDebug("External claims: {@claims}", externalClaims);
            }

            // lookup our user and external provider info
            var (user, provider, providerUserId, claims) = await FindUserFromExternalProviderAsync(result);
            if (user == null)
            {
                // this might be where you might initiate a custom workflow for user registration
                // in this sample we don't show how that would be done, as our sample implementation
                // simply auto-provisions new external user
                user = await AutoProvisionUserAsync(provider, providerUserId, claims);
            }

            // this allows us to collect any additional claims or properties
            // for the specific protocols used and store them in the local auth cookie.
            // this is typically used to store data needed for signout from those protocols.
            var additionalLocalClaims = new List<Claim>();
            var localSignInProps = new AuthenticationProperties();
            ProcessLoginCallback(result, additionalLocalClaims, localSignInProps, JwtClaimTypes.SessionId);
            
            // issue authentication cookie for user
            // we must issue the cookie maually, and can't use the SignInManager because
            // it doesn't expose an API to issue additional claims from the login workflow
            var principal = await _signInManager.CreateUserPrincipalAsync(user);
            additionalLocalClaims.AddRange(principal.Claims);
            var name = principal.FindFirst(JwtClaimTypes.Name)?.Value ?? user.Id;
            
            var isuser = new IdentityServerUser(user.Id)
            {
                DisplayName = name,
                IdentityProvider = provider,
                AdditionalClaims = additionalLocalClaims
            };

            await HttpContext.SignInAsync(isuser, localSignInProps);

            // delete temporary cookie used during external authentication
            await HttpContext.SignOutAsync(IdentityServerConstants.ExternalCookieAuthenticationScheme);

            // retrieve return URL
            var returnUrl = result.Properties.Items["returnUrl"] ?? "~/";

            // check if external login is in the context of an OIDC request
            var context = await _interaction.GetAuthorizationContextAsync(returnUrl);
            await _events.RaiseAsync(new UserLoginSuccessEvent(provider, providerUserId, user.Id, name, true, context?.Client.ClientId));

            if (context != null)
            {
                if (context.IsNativeClient())
                {
                    // The client is native, so this change in how to
                    // return the response is for better UX for the end user.
                    return this.LoadingPage("Redirect", returnUrl);
                }
            }

            return Redirect(returnUrl);
        }

        [HttpGet]
        public async Task<IActionResult> CancelMPass() 
        {
            var redirectURl = Configuration.GetValue<string>("MPassSaml:ServiceRootUrl");

            return Redirect(redirectURl);
        }

        private async Task<(ERPIdentityUser user, string provider, string providerUserId, IEnumerable<Claim> claims)>
            FindUserFromExternalProviderAsync(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            // try to determine the unique id of the external user (issued by the provider)
            // the most common claim type for that are the sub claim and the NameIdentifier
            // depending on the external provider, some other claim type might be used

            var userIdClaim = externalUser.FindFirst(JwtClaimTypes.Subject) ??
                              externalUser.FindFirst(ClaimTypes.NameIdentifier) ??
                              throw new Exception("Unknown userid");


            // remove the user id claim so we don't include it as an extra claim if/when we provision the user
            var claims = externalUser.Claims.ToList();
            claims.Remove(userIdClaim);

            var provider = result.Properties.Items["scheme"];
            var providerUserId = userIdClaim.Value;

            // find external user
            var user = await _userManager.FindByLoginAsync(provider, providerUserId);

            return (user, provider, providerUserId, claims);
        }

        private async Task<(ERPIdentityUser identityUser, string provider, IEnumerable<Claim> claims, string emailAdress)> FindUserFromMPassProviderAsync(AuthenticateResult result)
        {
            var externalUser = result.Principal;

            var claims = externalUser.Claims.ToList();

            var provider = externalUser.Identity.AuthenticationType;
            var emailAdress = claims.Find(x => x.Type == MPassClaimTypes.EmailAdress).Value;

            // find external user
            //var user = await _userManager.FindByNameAsync(claims.Find(x => x.Type == MPassClaimTypes.UserName).Value);
            var identityUser = await _identityDbContext.Users.FirstOrDefaultAsync(u => u.UserName == emailAdress);

            var nameId = _appDbContext.UserProfiles.First(x => x.Email == emailAdress).Idnp;
            if (string.IsNullOrEmpty(nameId))
            {
                throw new Exception("Null IDNP");
            }

            identityUser.UserName = nameId;

            return (identityUser, provider, claims, emailAdress);
        }

        private async Task<ERPIdentityUser> AutoProvisionUserAsync(string provider, string providerUserId, IEnumerable<Claim> claims)
        {
            // create a list of claims that we want to transfer into our store
            var filtered = new List<Claim>();

            // user's display name
            var name = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Name)?.Value ??
                claims.FirstOrDefault(x => x.Type == ClaimTypes.Name)?.Value;
            if (name != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Name, name));
            }
            else
            {
                var first = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.GivenName)?.Value ??
                    claims.FirstOrDefault(x => x.Type == ClaimTypes.GivenName)?.Value;
                var last = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.FamilyName)?.Value ??
                    claims.FirstOrDefault(x => x.Type == ClaimTypes.Surname)?.Value;
                if (first != null && last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first + " " + last));
                }
                else if (first != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, first));
                }
                else if (last != null)
                {
                    filtered.Add(new Claim(JwtClaimTypes.Name, last));
                }
            }

            // email
            var email = claims.FirstOrDefault(x => x.Type == JwtClaimTypes.Email)?.Value ??
               claims.FirstOrDefault(x => x.Type == ClaimTypes.Email)?.Value;
            if (email != null)
            {
                filtered.Add(new Claim(JwtClaimTypes.Email, email));
            }

            var user = new ERPIdentityUser
            {
                UserName = Guid.NewGuid().ToString(),
            };

            var identityResult = await _userManager.CreateAsync(user);
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            if (filtered.Any())
            {
                identityResult = await _userManager.AddClaimsAsync(user, filtered);
                if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);
            }

            identityResult = await _userManager.AddLoginAsync(user, new UserLoginInfo(provider, providerUserId, provider));
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            return user;
        }

        private async Task<ERPIdentityUser> AutoProvisionMPassUserAsync(string provider, IEnumerable<Claim> claims)
        {
            var newUser = new CreateUserDto()
            {
                FirstName = claims.First(x => x.Type == MPassClaimTypes.FirstName).Value,
                LastName = claims.First(x => x.Type == MPassClaimTypes.LastName).Value,
                Idnp = claims.First(x => x.Type == MPassClaimTypes.UserName).Value,
                Email = claims.First(x => x.Type == MPassClaimTypes.EmailAdress).Value,
                BirthDate = DateTime.Parse(claims.First(x => x.Type == MPassClaimTypes.BirthDate).Value),
                //PhoneNumber = claims.First(x => x.Type == MPassClaimTypes.PhoneNumber).Value,
                EmailNotification = true,
            };

            var userProfile = _mapper.Map<UserProfile>(newUser);
            userProfile.Contractor = new Contractor() { UserProfile = userProfile };

            var defaultRoles = _appDbContext.Modules
                .SelectMany(m => m.Roles.Where(r => r.IsAssignByDefault).Take(1))
                .ToList();

            var password = Generate();

            foreach (var role in defaultRoles)
            {
                userProfile.ModuleRoles.Add(new UserProfileModuleRole
                {
                    ModuleRole = role
                });
            };

            userProfile.Password = password;

            _appDbContext.UserProfiles.Add(userProfile);
            await _appDbContext.SaveChangesAsync();

            var identityUser = new ERPIdentityUser
            {
                Email = newUser.Email.Replace(" ", string.Empty),
                UserName = newUser.Email.Replace(" ", string.Empty),
            };

            var identityResult = await _userManager.CreateAsync(identityUser, password);
            if (!identityResult.Succeeded)
            {
                throw new Exception(identityResult.Errors.First().Description);
            }
            else
            {
                await _notificationService.PutEmailInQueue(new QueuedEmailData
                {
                    Subject = "Cont nou",
                    To = identityUser.Email,
                    HtmlTemplateAddress = "Templates/UserRegister.html",
                    ReplacedValues = new Dictionary<string, string>()
                        {
                            { "{FirstName}", userProfile.FullName }
                        }
                });


                var newIdentityUserProfile = new List<UserProfileIdentity>()
                {
                    new UserProfileIdentity() {UserProfileId = userProfile.Id, Type = provider, Identificator = identityUser.Id },
                    new UserProfileIdentity() {UserProfileId = userProfile.Id, Type = "local", Identificator = identityUser.Id }
            };

                userProfile.Identities.AddRange(newIdentityUserProfile);
                await _appDbContext.SaveChangesAsync();
            }

            var providerSesionId = claims.First(x => x.Type == MPassClaimTypes.SessionIndex).Value;

            identityResult = await _userManager.AddLoginAsync(identityUser, new UserLoginInfo(provider, providerSesionId, provider));
            if (!identityResult.Succeeded) throw new Exception(identityResult.Errors.First().Description);

            return identityUser;
        }

        // if the external login is OIDC-based, there are certain things we need to preserve to make logout work
        // this will be different for WS-Fed, SAML2p or other protocols
        private void ProcessLoginCallback(AuthenticateResult externalResult, List<Claim> localClaims, AuthenticationProperties localSignInProps, string sesionId)
        {
            // if the external system sent a session id claim, copy it over
            // so we can use it for single sign-out
            var sid = externalResult.Principal.Claims.FirstOrDefault(x => x.Type == sesionId);
            if (sid != null)
            {
                localClaims.Add(new Claim(JwtClaimTypes.SessionId, sid.Value));
            }

            // if the external provider issued an id_token, we'll keep it for signout
            var idToken = externalResult.Properties.GetTokenValue("id_token");
            if (idToken != null)
            {
                localSignInProps.StoreTokens(new[] { new AuthenticationToken { Name = "id_token", Value = idToken } });
            }
        }
        //Generate UserProfile password
        public string Generate()
        {
            return $"{RandomUppercase(1)}{RandomLowercase(5)}{RandomNumber(1)}{RandomSpecialChar(1)}";
        }
        public string RandomUppercase(int length)
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string RandomLowercase(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string RandomNumber(int length)
        {
            const string chars = "0123456789";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }

        public string RandomSpecialChar(int length)
        {
            const string chars = "!@#$%^&*()";
            return new string(Enumerable.Repeat(chars, length)
                .Select(s => s[new Random().Next(s.Length)]).ToArray());
        }
    }
}