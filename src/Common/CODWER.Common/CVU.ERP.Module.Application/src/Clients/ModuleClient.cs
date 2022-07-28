//using System;
//using System.Collections.Generic;
//using System.Threading;
//using System.Threading.Tasks;
//using CVU.ERP.Common.DataTransferObjects.Response;
//using CVU.ERP.Module.Application.Models;
//using CVU.ERP.Module.Common.Models;
//using Microsoft.Extensions.Options;
//using RestSharp;

//namespace CVU.ERP.Module.Application.Clients {
//    public class ModuleClient : IModuleClient {
//        private readonly IRestClient _restClient;

//        public ModuleClient (IRestClient restClient, IOptions<ModuleConfiguration> moduleConfiguration) {
//            _restClient = restClient;
//            _restClient.BaseUrl = new Uri (moduleConfiguration.Value.InternalGatewayBaseUrl);
//        }

//        public async Task<IEnumerable<ModulePermission>> GetPermissions (ApplicationModule module) {
//            const string resourceBasePath = "permission";
//            var resourceUrl = $"{module.InternalGatewayAPIPath}/{resourceBasePath}";

//            var request = new RestRequest (resourceUrl, DataFormat.Json);
//            var response = await _restClient.GetAsync<Response<IEnumerable<ModulePermission>>> (request, new CancellationToken ());
//            return response?.Data;
//        }
//    }
//}