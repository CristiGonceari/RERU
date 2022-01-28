using System;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Application.Exceptions;
using CVU.ERP.Module.Application.Models.Internal;
using CVU.ERP.Module.Common.Models;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CVU.ERP.Module.Application.Clients
{
    public class EvaluationClient : IEvaluationClient
    {
        private readonly IRestClient _restClient;
        const string UserProfileBasePath = "/internaluserprofile";
        private readonly IHttpContextAccessor _httpContextAccessor;

        public EvaluationClient(IRestClient restClient,
            IOptions<ModuleConfiguration> moduleConfiguration,
            IHttpContextAccessor httpContextAccessor)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(moduleConfiguration.Value.EvaluationClient.BaseUrl);
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task SyncUserProfile(BaseUserProfile userProfile)
        {
            var request = new RestRequest(UserProfileBasePath, DataFormat.Json);
            var json = JsonSerializer.Serialize(userProfile);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);

            foreach (var el in _httpContextAccessor?.HttpContext?.Request.Headers)
            {
                request.AddHeader(el.Key, el.Value);
            }

            var response = await _restClient.PostAsync<Response<Unit>>(request, new CancellationToken());

            if (!response.Success)
            {
                throw new EvaluationClientResponseNotSuccessfulException(response.Messages);
            }
        }
    }
}
