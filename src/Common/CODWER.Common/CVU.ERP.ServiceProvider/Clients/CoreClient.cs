using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.DataTransferObjects.Users;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.ServiceProvider.Exceptions;
using CVU.ERP.ServiceProvider.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CVU.ERP.ServiceProvider.Clients
{
    public class CoreClient : ICoreClient
    {
        private readonly IRestClient _restClient;
        private readonly IHttpContextAccessor _httpContextAccessor;
        const string UserProfileBasePath = "/user-profile";
        const string ModuleBasePath = "/module";

        public CoreClient(IRestClient restClient,
            IOptions<ModuleConfiguration> moduleConfiguration,
            IHttpContextAccessor httpContextAccessor)
        {
            _restClient = restClient;
            _httpContextAccessor = httpContextAccessor;
            _restClient.BaseUrl = new Uri(moduleConfiguration.Value.CoreClient.BaseUrl);
        }

        public async Task<ApplicationUser> GetApplicationUser(string coreUserProfileId)
        {
            var resourceUrl = $"{UserProfileBasePath}/{coreUserProfileId}";
            var request = NewJsonRequest(resourceUrl);

            var response = await _restClient.GetAsync<Response<ApplicationUser>>(request, new CancellationToken());
            if (!response.Success)
            {
                throw new CoreClientResponseNotSuccessfulException(response.Messages);
            }
            return response?.Data;
        }

        //public async Task<bool> ExistUserInCore(string coreUserProfileId)
        //{
        //    var resourceUrl = $"{UserProfileBasePath}/{coreUserProfileId}";
        //    var request = NewJsonRequest(resourceUrl);

        //    var response = await _restClient.GetAsync<Response<ApplicationUser>>(request, new CancellationToken());

        //    return response.Success;
        //}

        //public async Task<ApplicationUser> CreateUserProfile(InternalUserProfileCreate userProfileDto)
        //{
        //    var request = NewJsonRequest(UserProfileBasePath);
        //    var json = JsonSerializer.Serialize(userProfileDto);
        //    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

        //    var response = await _restClient.PostAsync<Response<ApplicationUser>>(request, new CancellationToken());

        //    if (!response.Success)
        //    {
        //        throw new CoreClientResponseNotSuccessfulException(response.Messages);
        //    }
        //    return response?.Data;

        //}

        //public async Task<ApplicationUser> CreateUser(CreateUserDto userDto)
        //{
        //    var request = NewJsonRequest(UserProfileBasePath);

        //    request.AddJsonBody(userDto);
        //    var response = await _restClient.PostAsync<Response<ApplicationUser>>(request, new CancellationToken());

        //    if (!response.Success)
        //    {
        //        throw new CoreClientResponseNotSuccessfulException(response.Messages);
        //    }
        //    return response?.Data;
        //}

        public async Task<ApplicationUser> GetApplicationUserByIdentity(string id, string identityProvider)
        {
            var resourceUrl = $"{UserProfileBasePath}/{identityProvider}/{id}";
            var request = new RestRequest(resourceUrl, DataFormat.Json);

            var response = await _restClient.GetAsync<Response<ApplicationUser>>(request, new CancellationToken());

            if (!response.Success)
            {
                throw new CoreClientResponseNotSuccessfulException(response.Messages);
            }
            return response?.Data;
        }

        //public async Task ResetPassword(string coreUserProfileId)
        //{
        //    var request = NewRequest($"{UserProfileBasePath}/{coreUserProfileId}/deactivate");

        //    var response = await _restClient.PatchAsync<Response>(request);

        //    if (!response.Success)
        //    {
        //        throw new CoreClientResponseNotSuccessfulException(response.Messages);
        //    }
        //}

        //public async Task DeactivateUserProfile(string coreUserProfileId)
        //{
        //    var request = NewRequest($"{UserProfileBasePath}/{coreUserProfileId}/deactivate");

        //    var response = await _restClient.PatchAsync<Response>(request);

        //    if (!response.Success)
        //    {
        //        throw new CoreClientResponseNotSuccessfulException(response.Messages);
        //    }
        //}

        public Task<ApplicationUser> GetApplicationUserByIdentity(string identity)
        {
            return GetApplicationUserByIdentity(identity, null);
        }


        public async Task<List<ModuleRolesDto>> GetModuleRoles()
        {
            var resourceUrl = $"{ModuleBasePath}";
            var request = NewJsonRequest(resourceUrl);

            var response = await _restClient.GetAsync<Response<List<ModuleRolesDto>>>(request, new CancellationToken());

            if (!response.Success)
            {
                throw new CoreClientResponseNotSuccessfulException(response.Messages);
            }
            return response?.Data;
        }

        private RestRequest NewJsonRequest(string resource)
        {
            var request = new RestRequest(resource, DataFormat.Json);
            request.AddHeaders(GetHeaders());

            return request;
        }

        private RestRequest NewRequest(string resource)
        {
            var request = new RestRequest(resource);
            request.AddHeaders(GetHeaders());

            return request;
        }

        private List<KeyValuePair<string, string>> GetHeaders()
        {
            return _httpContextAccessor?.HttpContext?.Request.Headers.ToList()
                .Select(h => new KeyValuePair<string, string>(h.Key, h.Value)).ToList();
        }
    }
}