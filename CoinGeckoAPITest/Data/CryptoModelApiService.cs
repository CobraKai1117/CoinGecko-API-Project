using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using CoinGeckoAPITest.Models;
using System.Text.Json;
using Newtonsoft.Json;

namespace CoinGeckoAPITest.Data
{
    public class CryptoModelApiService:ICryptoModelApiService
    {
        private static readonly HttpClient client;

        dynamic test;

        static CryptoModelApiService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.coingecko.com")
            };
        }

        public async Task<CryptoModel> GetCryptos(string cryptoName,string currency)
        {
            var url = string.Format("/api/v3/simple/price?ids={0}&vs_currencies={1}",cryptoName,currency);

            var result = new CryptoModel();

            var response = await client.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                // result = JsonSerializer.Deserialize<List<CryptoModel>>(stringResponse, new JsonSerializerOptions() { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

               test = JsonConvert.DeserializeObject<CryptoModel>(stringResponse);

                result = test;
               
            }

            else { throw new HttpRequestException(response.ReasonPhrase); }

            return test;

            
        }

       
    }
}
