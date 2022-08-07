using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinGeckoAPITest.Models;

namespace CoinGeckoAPITest.Data
{
   public  interface ICryptoModelApiService
    {
        Task<cryptoComparisonModel> GetCrypoPrice(string cryptoName, string currency);

        Task<List<String>> GetSupportedCurrencies();

        Task<Dictionary<string, CryptoInformationModel>> GetCryptoInformation();
    }
}
