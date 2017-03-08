using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Easynvest.Temperature.ConsoleApp.Bot
{
    public static class RobotDataSender
    {
        private static Uri Endpoint { get { return new Uri(ConfigurationManager.AppSettings["endpoint"]); }}
        private static string RequestURI { get; set; }

        public static async void Send(string temperature = null)
        {
            if (!string.IsNullOrEmpty(temperature))
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = Endpoint;

                    StringContent content = new StringContent(string.Empty);
                    // HTTP POST
                    HttpResponseMessage response = await client.PostAsync(RequestURI + temperature, content);
                }
            }
        }
    }
}
