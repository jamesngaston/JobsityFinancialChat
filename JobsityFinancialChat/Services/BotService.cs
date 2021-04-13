using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web;
using CsvHelper;
using JobsityFinancialChat.Interfaces;
using JobsityFinancialChat.Models;
using JobsityFinancialChat.SignalR;
using Microsoft.AspNet.SignalR;
using Newtonsoft.Json;

namespace JobsityFinancialChat.Services
{
    public class BotService : IBotService
    {
        public BotService()
        {
            _hubContext = GlobalHost.ConnectionManager.GetHubContext("SignalRChat");

        }

        IHubContext _hubContext;

        public async Task ParseMessage(string message)
        {
            try
            {
                if (message.ToLower().StartsWith("/stock="))
                {
                    string code = message.Replace("/stock=", "");
                    using (HttpResponseMessage response = new HttpClient().GetAsync($"https://localhost:44322/Api/Bot/Stock?code=" + code).Result)
                    {

                        if (response.StatusCode != HttpStatusCode.OK)
                        {
                            throw new Exception("code " + (int)response.StatusCode + " - " + response.Content.ReadAsStringAsync().Result) ;
                        }
                        string jsonString = response.Content.ReadAsStringAsync().Result;
                        Stock stock = JsonConvert.DeserializeObject<Stock>(jsonString);
                        string successMessage = stock.Symbol + " quote is $" + stock.Close + " per share";
                        await SendMessage(successMessage);
                    }
                }
            }
            catch (Exception e)
            {
                while (e.InnerException != null)
                {
                    e = e.InnerException;
                }
                string errorMessage = "ERROR: " + e.Message;
                await SendMessage(errorMessage);
            }
        }

        public async Task SendMessage(string message)
        {
            await _hubContext.Clients.All.broadcastMessage("Financial Bot", message, DateTime.Now.ToString("G"));
        }
    }
}