using CsvHelper;
using JobsityFinancialChat.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace JobsityFinancialChat.Controllers
{
    public class BotController : ApiController
    {
        public BotController()
        {
        }


        //
        // GET: /Bot/
        public String Index()
        {
            return "Hello World Index";
        }


        //
        // GET: /Bot/Hello
        public String Hello()
        {
            return "Hello World";
        }

        [HttpGet]
        public IHttpActionResult Stock(string code)
        {
            try
            {
                using (HttpResponseMessage response = new HttpClient().GetAsync($"https://stooq.com/q/l/?s=" + code + "&f=sd2t2ohlcv&h&e=csv").Result)
                {
                    Stock stockResponse = new Stock();

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        return Content(response.StatusCode, "stooq.com returned code " + response.StatusCode + ".");
                    }
                    try
                    {
                        string stringResponse = response.Content.ReadAsStringAsync().Result;
                        StringReader streamResponse = new StringReader(stringResponse);
                        CsvReader csv = new CsvReader(streamResponse, CultureInfo.InvariantCulture);
                        stockResponse = csv.GetRecords<Stock>().FirstOrDefault();
                    }
                    catch
                    {
                        return Content(HttpStatusCode.BadRequest, "Invalid stock code.");
                    }
                    return Ok(stockResponse);
                }
            }
            catch
            {
                return Content(HttpStatusCode.Forbidden, "Too many requests in a short time. Wait a minute before requesting again.");
            }
        }
    }
}
