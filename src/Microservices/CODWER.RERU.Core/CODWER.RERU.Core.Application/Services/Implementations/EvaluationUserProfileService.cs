using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using CVU.ERP.Common.DataTransferObjects.Response;
using CVU.ERP.Module.Application.Exceptions;
using CVU.ERP.Module.Application.Models.Internal;
using CVU.ERP.Module.Common.Models;
using MediatR;
using Microsoft.Extensions.Options;
using RestSharp;

namespace CODWER.RERU.Core.Application.Services.Implementations
{
    public class EvaluationUserProfileService : IEvaluationUserProfileService
    {
        private readonly IRestClient _restClient;
        const string UserProfileBasePath = "/internaluserprofile";

        public EvaluationUserProfileService(IRestClient restClient, IOptions<ModuleConfiguration> moduleConfiguration)
        {
            _restClient = restClient;
            _restClient.BaseUrl = new Uri(moduleConfiguration.Value.EvaluationClient.BaseUrl);
        }

        public async Task Sync(BaseUserProfile userProfile)
        {
            var request = new RestRequest(UserProfileBasePath, DataFormat.Json);
            var json = JsonSerializer.Serialize(userProfile);
            request.AddParameter("application/json; charset=utf-8", json, ParameterType.RequestBody);
            var response = await _restClient.PostAsync<Response<Unit>>(request, new CancellationToken());

            if (!response.Success)
            {
                throw new EvaluationClientResponseNotSuccessfulException(response.Messages);
            }
        }
    }
}
