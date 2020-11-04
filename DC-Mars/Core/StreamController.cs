using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DC_Mars.Debug;
using System.Net.Http.Headers;

namespace DC_Mars.Core
{
    internal class StreamController
    {
        private static readonly Logging logger = new Logging();
        private static readonly HttpClient client = new HttpClient();
        private static readonly string _url = "https://piczel.tv/api/streams?followedStreams=false&live_only=false&sfw=false";

        public static async Task fetchPiczelData()
        {
            Piczel piczel = new Piczel();
            var bodyContent = JsonConvert.SerializeObject(piczel);
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            var content = new StringContent(bodyContent.ToString(), Encoding.UTF8, "application/json");
            content.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            var response = await client.PostAsync(_url, content);

            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                try
                {
                    var result = JsonConvert.DeserializeObject<Piczel>(jsonString);
                }
                catch (Exception e)
                {
                    await logger.LogCustom($"Error: {e.Message}", 2);
                }
            }
        }

        public static void AddStreamer(string piczelUser, string piczelId)
        {
        }
    }
}