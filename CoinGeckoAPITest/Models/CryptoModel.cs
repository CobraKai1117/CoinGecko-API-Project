using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace CoinGeckoAPITest.Models
{
    public class CryptoModel
    {

        [JsonExtensionData]
        public Dictionary<string, JToken> Items { get; set; }


    }
}
