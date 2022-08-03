using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace CoinGeckoAPITest.Models
{
    public class CryptoInformationModel
    {
        [JsonProperty("id")]
        public string Id { get; set; }
        [JsonProperty("symbol")]
        public string Symbol { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

    }
}
