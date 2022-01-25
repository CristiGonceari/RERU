using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;

namespace CODWER.RERU.Personal.Application.Interfaces
{
    public interface IAzureStorageClient
    {
        IEnumerable<string> GetResourceList(string Container, IEnumerable<string> ImageNames);

        string GetResource(string Container, string ResourceName);

        string GetTextResource(string Container, string ResourceName);

        Task<KeyValuePair<string, string>> SaveResource(string Container, Stream Resource, string Extension, string ContentType);

        Task<bool> DeleteResource(string Container, string ResourceId);
    }
}
