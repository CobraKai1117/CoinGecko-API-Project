using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;



namespace CoinGeckoAPITest.Models
{
    public class cryptoComparisonModel
    {

        [JsonExtensionData]
        public Dictionary<string, JToken> Items { get; set; } // Dictionary name of crypto as key and its price as the value. Contains one crypto and one comparision currency (i.e. Kusama/USD)

        public List<String> currency { get; set; } // List of all comparison currencies   // NOTE TO SELF WILL NEED TO MAKE THIS A SINGLE STRING RETRIEVED FROM GET SUPPORTED CURRENCIES

       


    }
}
