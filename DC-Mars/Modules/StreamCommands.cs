using DC_Mars.Debug;
using System;
using System.Text;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Collections;
using System.Diagnostics;
using System.Linq;
using Newtonsoft.Json;
using Discord.Commands;

namespace DC_Mars.Modules
{
    public class StreamCommands : ModuleBase
    {
        private static readonly Logging logger = new Logging();
        private static readonly HttpClient client = new HttpClient();
        private static readonly string _url = "https://piczel.tv/api/streams?followedStreams=false&live_only=false&sfw=false";

        public class OfflineImage
        {
            public string url { get; set; }
        }

        public class Banner2
        {
            public object url { get; set; }
        }

        public class Banner
        {
            public Banner2 banner { get; set; }
        }

        public class Preview
        {
            public object url { get; set; }
        }

        public class Basic
        {
            public bool listed { get; set; }
            public bool allowAnon { get; set; }
            public bool notifications { get; set; }
        }

        public class Recording
        {
            public bool enabled { get; set; }
            public bool download { get; set; }
            public int timelapse_speed { get; set; }
            public bool watermark_timelapse { get; set; }
            public bool gen_timelapse { get; set; }
        }

        public class Private
        {
            public bool enabled { get; set; }
            public bool moderated { get; set; }
        }

        public class Emails
        {
            public bool enabled { get; set; }
        }

        public class Settings
        {
            public Basic basic { get; set; }
            public Recording recording { get; set; }
            public Private @private { get; set; }
            public Emails emails { get; set; }
        }

        public class Avatar
        {
            public string url { get; set; }
        }

        public class Gallery
        {
            public string profile_description { get; set; }
        }

        public class User
        {
            public int id { get; set; }
            public string username { get; set; }

            [JsonProperty("premium?")]
            public bool Premium { get; set; }

            public Avatar avatar { get; set; }
            public string role { get; set; }
            public Gallery gallery { get; set; }
            public int follower_count { get; set; }
        }

        public class PiczelRoot
        {
            public int id { get; set; }
            public string title { get; set; }
            public object description { get; set; }
            public string rendered_description { get; set; }
            public int follower_count { get; set; }
            public bool live { get; set; }
            public DateTime live_since { get; set; }

            [JsonProperty("isPrivate?")]
            public bool IsPrivate { get; set; }

            public string slug { get; set; }
            public OfflineImage offline_image { get; set; }
            public Banner banner { get; set; }
            public object banner_link { get; set; }
            public Preview preview { get; set; }
            public bool adult { get; set; }
            public bool in_multi { get; set; }
            public string parent_streamer { get; set; }
            public Settings settings { get; set; }
            public string viewers { get; set; }
            public string username { get; set; }
            public List<object> tags { get; set; }
            public int bitrate { get; set; }
            public User user { get; set; }
            public List<object> recordings { get; set; }
        }

        [Command("loadpiczelAPI")]
        public async Task loadAPI()
        {
            Stopwatch sw = Stopwatch.StartNew();
            Stopwatch sw1 = Stopwatch.StartNew();
            System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
            HttpResponseMessage response = await client.GetAsync("https://piczel.tv/api/streams?followedStreams=false&live_only=false&sfw=false");
            if (!response.IsSuccessStatusCode)
            {
                await logger.LogCustom($"[CORE-ERR] Error: {response.StatusCode}\nReason: {response.ReasonPhrase}", 2);
                await Context.Channel.SendMessageAsync($"Error fetching Web API.\n" +
                                                        $"Server returned: {response.StatusCode}\n" +
                                                        $"{response.ReasonPhrase}");
            }
            sw.Stop();
            await Context.Channel.SendMessageAsync($"[DEBUG]------\n" +
                                                   $"Server returned: {response.StatusCode}\n" +
                                                   $"{response.ReasonPhrase}\n\n" +
                                                   $"API Download took: {sw.ElapsedMilliseconds}ms.", false);
            if (response != null)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                try
                {
                    PiczelRoot result = JsonConvert.DeserializeObject<PiczelRoot>(jsonString);
                    await Context.Channel.SendMessageAsync("[CORE-DBG] Success. API information loaded into Mars.");
                }
                catch (Exception e)
                {
                    await logger.LogCustom($"Error: {e.Message}", 2);
                    await Context.Channel.SendMessageAsync($"[CORE-ERR] Critical Exception:\n {e.Message}");
                }
            }
            sw1.Stop();
            await Context.Channel.SendMessageAsync($"[DEBUG] Process took {sw1.ElapsedMilliseconds}ms to complete.");
        }
    }
}