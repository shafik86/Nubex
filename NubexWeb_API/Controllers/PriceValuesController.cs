using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Nubex.Service.IService;
using System.Net.Http;
using Nubex_Models;
using System.Xml.Serialization;

namespace NubexWeb_API.Controllers
{
        [EnableCors("PriceApi")]
    [Route("api/[controller]")]
    [ApiController]
    public class PriceValuesController : ControllerBase
    {
        public PriceValuesController(HttpClient http)
        {
            _http = http;
        }

        public HttpClient _http { get; }
        public golds AllPrices { get; set; }
        public Prices prices { get; set; } = new Prices
        {
            goldPrice_Myr = 0,
            silverPrice_Myr = 0,
        };

        [HttpGet]
        public async Task<golds> GetCurrentPrice()
        {
            var url = new Uri("https://gold-feed.com/paid/d7d6s6d66k4j4658e6d6cds638940e/xmlgold_myr.php");
            //var url = new Uri("https://gold-feed.com/paid/d7d6s6d66k4j4658e6d6cds638940e/xmlgold_usd.php");
            var cts = new CancellationTokenSource(TimeSpan.FromSeconds(5));
            try
            {
                var result = await _http.GetStringAsync(url, cts.Token);
                if (result != null)
                {

                    var xmlserial = new XmlSerializer(typeof(golds));
                    using (var reader = new StringReader(result))
                    {
                        AllPrices = (golds)xmlserial.Deserialize(reader);

                    }

                    //Console.WriteLine(result);
                    return AllPrices;

                }
            }
            catch (Exception)
            {

                Console.WriteLine("Xdapat");
            }
            return new golds();
        }


    }
}
