using Nancy.Json;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace BlockLogic.TrickleSystem.PaymentProcessing
{
    public class CurrencyConverter
    {

        public decimal FiatToBTC(string fiat, double amount)
        {
            decimal rate;
            var code = $"{fiat}_BTC";
            String BASE_URI = "http://free.currconv.com";
            var url = $"{BASE_URI}/api/v7/convert?q={fiat}_BTC&compact=y&apiKey=81428af075e9b2a0d459";
            var webClient = new WebClient();

            var jsonData = webClient.DownloadString(url);
            //var jsonObject = new JavaScriptSerializer().Deserialize<Dictionary<string, Dictionary<string, decimal>>>(jsonData);
            rate = JObject.Parse(jsonData).First.First["val"].ToObject<decimal>();
            //var result = jsonObject[code];
            //rate = result["val"];

            decimal money = (decimal)amount * rate;

            return money;

        }


    }


}
