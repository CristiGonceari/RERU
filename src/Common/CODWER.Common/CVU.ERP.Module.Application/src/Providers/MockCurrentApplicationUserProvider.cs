//using System.IO;
//using System.Threading.Tasks;
//using CVU.ERP.Module.Application.Models;
//using Newtonsoft.Json;

//namespace CVU.ERP.Module.Application.Providers
//{
//    public class MockCurrentApplicationUserProvider : ICurrentApplicationUserProvider
//    {
//        public bool IsAuthenticated => !string.IsNullOrEmpty(IdentityId);

//        public string IdentityId => Get().Result?.Id;
//        public string IdentityProvider => "mock";
//        public async Task<ApplicationUser> Get()
//        {
//            try
//            {
//                var user = JsonConvert.DeserializeObject<ApplicationUser>(File.ReadAllText(@"Mock/application-user.json"));
//                return await Task.FromResult(user);
//            }
//            catch { }
//            return null;
//        }
//    }
//}