﻿using CoinGeckoAPITest.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using CoinGeckoAPITest.Data;

namespace CoinGeckoAPITest.Controllers
{
    public class HomeController : Controller
    {
        
        private readonly ICryptoModelApiService _cryptoApiService;

        public HomeController(ICryptoModelApiService cryptoApiService)
        {
            _cryptoApiService = cryptoApiService;
        }

        public async Task<IActionResult> Index(string cryptoName, string currency)
        {
            CryptoModel cryptoModels = new CryptoModel();
            if(cryptoName == null || cryptoName == " "|| currency == null || currency == " " ) { cryptoName = "Kusama"; currency = "btc"; }

            string cryptoPair = cryptoName + "/" + currency.ToUpper();

            ViewBag.cryptoPair = cryptoPair; // Passed to view, represents crypto/currency pair being compared

            cryptoModels = await _cryptoApiService.GetCryptos(cryptoName, currency);

            Dictionary<string, CryptoInformationModel> cryptoSearchResults;

            cryptoSearchResults = await _cryptoApiService.GetCryptoInformation();


            string result = cryptoModels.Items.Values.ToString();
            

            return View(cryptoModels);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
