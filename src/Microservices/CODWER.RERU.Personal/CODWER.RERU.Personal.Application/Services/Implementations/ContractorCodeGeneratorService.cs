using System;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;
using CODWER.RERU.Personal.Data.Persistence.Context;

namespace CODWER.RERU.Personal.Application.Services.Implementations
{
    public class ContractorCodeGeneratorService : IContractorCodeGeneratorService
    {
        private readonly AppDbContext _appDbContext;

        public ContractorCodeGeneratorService(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public Task<string> Next() // total 10 digits
        {
            var currentDate = DateTime.Now;
            var firstDigit = "0";   //1 digit
            var year = currentDate.Year.ToString().Substring(2);    //  2 digit
            var month = currentDate.Month.ToString("00");           //  2 digit

            var currentGeneratedCode = string.Empty;

            while (!IsUniqueCode(currentGeneratedCode))
            {
                var randomNumber = LongRandom(); // 5 digit
                currentGeneratedCode = $"{firstDigit}{year}{month}{randomNumber}";
            }

            return Task.FromResult(currentGeneratedCode);
        }

        private string LongRandom()
        {
            using RNGCryptoServiceProvider provider = new RNGCryptoServiceProvider();
            var byteArray = new byte[4];
            provider.GetBytes(byteArray);

            return BitConverter.ToUInt32(byteArray, 0).ToString("00000").Substring(0, 5);
        }


        private bool IsUniqueCode(string code)
        {
            if (string.IsNullOrEmpty(code))
            {
                return false;
            }

            return !_appDbContext.Contractors.Any(x => x.Code == code);
        }
    }
}
