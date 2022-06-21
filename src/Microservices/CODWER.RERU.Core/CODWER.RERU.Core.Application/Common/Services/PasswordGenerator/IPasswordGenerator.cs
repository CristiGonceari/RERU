using System.Threading.Tasks;

namespace CODWER.RERU.Core.Application.Common.Services.PasswordGenerator
{
    public interface IPasswordGenerator
    {
        string Generate();
        string RandomEmailCode();
    }
}