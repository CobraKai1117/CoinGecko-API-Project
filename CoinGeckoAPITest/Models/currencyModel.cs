using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinGeckoAPITest.Models
{
    public class currencyModel
    {
        public string currencyName { get; set; }

        public Boolean hasSymbol { get; set; } // Checks to see if currency has a symbol such as $. This will be used for formatting purposes when displaying pricing

        public string nameAbbreviation { get; set; } // abbreviation such as USD will be used for retrieving currency pair from API.


        public currencyModel() 
        {
            hasSymbol = false;
        }



    }
}
