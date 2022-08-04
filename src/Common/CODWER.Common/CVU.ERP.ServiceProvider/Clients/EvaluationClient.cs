using System;
using System.Collections.Generic;
using System.Linq;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Common.DataTransferObjects.TestDatas;
using CVU.ERP.Module.Common.Models;
using CVU.ERP.ServiceProvider.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;
using JsonSerializer = System.Text.Json.JsonSerializer;

namespace CVU.ERP.ServiceProvider.Clients
{
    public class EvaluationClient : IEvaluationClient
    {
        private readonly IRestClient _restClient;
        private readonly ICurrentApplicationUserProvider _currentApplicationUserProvider;
        const string UserProfileBasePath = "/internaluserprofile";
        private const string TestBasePath = "/internalgettestid";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EvaluationClient(IRestClient restClient,
            IOptions<ModuleConfiguration> moduleConfiguration,
            IHttpContextAccessor httpContextAccessor, 
            ICurrentApplicationUserProvider currentApplicationUserProvider)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(moduleConfiguration.Value.EvaluationClient.BaseUrl);
            _httpContextAccessor = httpContextAccessor;
            _currentApplicationUserProvider = currentApplicationUserProvider;
        }

        //public async Task SyncUserProfile(BaseUserProfile userProfile)
        //{
        //    var request = NewJsonRequest(UserProfileBasePath);
        //    var json = JsonSerializer.Serialize<BaseUserProfile>(userProfile);
        //    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

        //    //var response = await _restClient.PostAsync<Response<Unit>>(request, new CancellationToken());
        //    var response = await _restClient.PutAsync<Response<Unit>>(request, new CancellationToken());

        //    if (!response.Success)
        //    {
        //        throw new EvaluationClientResponseNotSuccessfulException(response.Messages);
        //    }
        //}

        //public async Task SyncUserProfileByAnonymous(BaseUserProfile userProfile)
        //{
        //    var request = NewJsonRequest($"{UserProfileBasePath}/by-anonymous");
        //    var json = JsonSerializer.Serialize<BaseUserProfile>(userProfile);
        //    request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

        //    var response = await _restClient.PostAsync<Response<Unit>>(request, new CancellationToken());

        //    if (!response.Success)
        //    {
        //        throw new EvaluationClientResponseNotSuccessfulException(response.Messages);
        //    }
        //}

        //public async Task<TestDataDto> GetTestIdToStartTest()
        //{
        //    var user = await _currentApplicationUserProvider.Get();

        //    var request = NewJsonRequest($"{TestBasePath}/{user.Id}");

        //    var response = await _restClient.GetAsync<Response<TestDataDto>>(request, new CancellationToken());

        //    if (!response.Success)
        //    {
        //        throw new EvaluationClientResponseNotSuccessfulException(response.Messages);
        //    }

        //    return response.Data;
        //}

        private RestRequest NewJsonRequest(string resource)
        {
            var request = new RestRequest(resource, DataFormat.Json);
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
