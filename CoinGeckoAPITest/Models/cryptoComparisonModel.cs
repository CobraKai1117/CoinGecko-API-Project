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
        public Dictionary<string, JToken> Items { get; set; } // Dictionary name of crypto as key and its price as the value.

        public List<String> currency { get; set; } // List of comparison currencies

       


    }
}
