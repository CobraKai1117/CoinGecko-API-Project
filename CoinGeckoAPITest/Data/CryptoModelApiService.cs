using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Net.Http;
using CoinGeckoAPITest.Models;
using System.Text.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Text.RegularExpressions;
using System.Web;



namespace CoinGeckoAPITest.Data
{
    public class CryptoModelApiService : ICryptoModelApiService
    {
        private static readonly HttpClient client;

        dynamic deserializedResults;

        static CryptoModelApiService()
        {
            client = new HttpClient()
            {
                BaseAddress = new Uri("https://api.coingecko.com")
            };
        }

        public async Task<List<string>> GetSupportedCurrencies()
        {
            var supportedCurrencyURL = string.Format("/api/v3/simple/supported_vs_currencies");

            List<String> supportedCurrencies = new List<String>();

            var scResponse = await client.GetAsync(supportedCurrencyURL);

            if (scResponse.IsSuccessStatusCode)
            {
                var stringResponse = await scResponse.Content.ReadAsStringAsync();


                supportedCurrencies = JsonConvert.DeserializeObject<List<String>>(stringResponse);
            }

            else { throw new HttpRequestException(scResponse.ReasonPhrase); }

            return supportedCurrencies;

        }

        public async Task<Dictionary<string, CryptoInformationModel>> GetCryptoInformation() 
        {

            var cryptoURL = string.Format("api/v3/coins/list?include_platform=false");

            var response = await client.GetAsync(cryptoURL);

            if (response.IsSuccessStatusCode) 
            {
                var stringResponse = await response.Content.ReadAsStringAsync();

                JArray cryptoInformation = JArray.Parse(stringResponse);

                Dictionary<string, CryptoInformationModel> cryptoContainer = new Dictionary<string, CryptoInformationModel>();

                foreach (JObject crypto in cryptoInformation.Children<JObject>()) 
                {
                    CryptoInformationModel cryptoAdded = new CryptoInformationModel();


                    foreach (JProperty cryptoProperty in crypto.Properties()) 
                    {
                        if (cryptoProperty.Name == "id") { cryptoAdded.Id = (string)cryptoProperty.Value; }
                        if (cryptoProperty.Name == "symbol") { cryptoAdded.Symbol = (string)cryptoProperty.Value;}
                        if (cryptoProperty.Name == "name") { cryptoAdded.Name = (string)cryptoProperty.Value; }

                    }

                    cryptoContainer.Add(cryptoAdded.Name,cryptoAdded);
                }

                return cryptoContainer;
            }

            else { throw new HttpRequestException(response.ReasonPhrase); }
        }
         
  
  

        public async Task<cryptoComparisonModel> GetCrypoPrice(string cryptoName,string currency)  // Returns price of crypto based on currency being compared
        {
            var url = string.Format("/api/v3/simple/price?ids={0}&vs_currencies={1}",cryptoName,currency);

           cryptoComparisonModel result = new cryptoComparisonModel();

            var response = await client.GetAsync(url);


            if (response.IsSuccessStatusCode)
            {

                var stringResponse = await response.Content.ReadAsStringAsync();

                JObject cryptoInfo = JObject.Parse(stringResponse); // used to convert JSON to CryptoModel

                string cryptoValueFormat = cryptoInfo.GetValue(cryptoName.ToLower()).ToString(); // Used for proper formatting of output

                Regex criteriaToReplace = new Regex("[]{}:;,\t\r]|[\n]{2}");

               cryptoValueFormat = criteriaToReplace.Replace(cryptoValueFormat, "");

                cryptoValueFormat = cryptoValueFormat.Replace("\"", "");

                cryptoValueFormat = cryptoValueFormat.Replace(currency, "");

                decimal cryptoPrice = Convert.ToDecimal(cryptoValueFormat);

                if (currency == "usd") 
                { cryptoValueFormat = cryptoPrice.ToString(cryptoPrice % 1 == 0 ? "C0" : "C");}

                cryptoInfo[cryptoName.ToLower()] = cryptoValueFormat; // Sets value of JObject

                result = cryptoInfo.ToObject<cryptoComparisonModel>();  // converts JObject to CryptoModel

            }

            else { throw new HttpRequestException(response.ReasonPhrase); }

            result.currency = await GetSupportedCurrencies();

            return result;

            
        }

       
    }
}
