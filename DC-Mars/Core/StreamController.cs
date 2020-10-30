using System;
using System.Collections.Generic;
using System.Text;
using System.Net.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;
using DC_Mars.Debug;

namespace DC_Mars.Core
{
    internal class StreamController
    {
        private static readonly Logging logger = new Logging();
        private static readonly HttpClient client = new HttpClient();

        public static async Task fetchPiczelData(int id)
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                HttpResponseMessage response = await client.GetAsync(@"https://piczel.tv/api/streams/" + id);
                response.EnsureSuccessStatusCode();
            }
            catch (HttpRequestException e)
            {
                await logger.LogCustom(e.Message, 1);
            }
        }

        public static void AddStreamer(string piczelUser, string piczelId)
        {
        }
    }
}