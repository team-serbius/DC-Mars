using DC_Mars.Debug;
using Discord.Commands;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

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

        [Serializable]
        public class Streamer
        {
            public string username { get; set; }
            public string service { get; set; }
        }

        public List<PiczelRoot> piczelData;

        private string streamersAggregated = "";

        [Command("piczelAPI")]
        public async Task PiczelAPI()
        {
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls11 | System.Net.SecurityProtocolType.Tls;
                HttpResponseMessage response = await client.GetAsync("https://piczel.tv/api/streams?followedStreams=false&live_only=false&sfw=false");
                if (!response.IsSuccessStatusCode)
                {
                    await logger.LogCustom($"[CORE-ERR] Error: {response.StatusCode}\nReason: {response.ReasonPhrase}", 2).ConfigureAwait(false);
                    await Context.Channel.SendMessageAsync($"Error fetching Web API.\n" +
                                                           $"Server returned: {(int)response.StatusCode} - {response.ReasonPhrase}").ConfigureAwait(false);
                }
                await Context.Channel.SendMessageAsync($"[DEBUG]------\n" +
                                                       $"Server returned: {response.StatusCode} - {response.ReasonPhrase}").ConfigureAwait(false);
                if (response != null)
                {
                    //var isLooped = true;
                    var jsonString = await response.Content.ReadAsStringAsync();

                    /*if (isLooped)
                    {
                        piczelData = JsonConvert.DeserializeObject<List<PiczelRoot>>(jsonString);
                        if (loadAPI(piczelData))
                        {
                            await Context.Channel.SendMessageAsync($"[DEBUG] API succesfully loaded into memory and has {piczelData.Count} elements.").ConfigureAwait(false);
                        }
                        else
                        {
                            await Context.Channel.SendMessageAsync("[CORE-ERR] Error: Could not load API in memory.");
                            isLooped = false;
                        }
                    }*/
                    piczelData = JsonConvert.DeserializeObject<List<PiczelRoot>>(jsonString);
                    if (loadAPI(piczelData))
                    {
                        await Context.Channel.SendMessageAsync($"[DEBUG] API succesfully loaded into memory and has {piczelData.Count} elements.").ConfigureAwait(false);
                    }
                    else
                    {
                        await Context.Channel.SendMessageAsync("[CORE-ERR] Error: Could not load API in memory.");
                        //isLooped = false;
                    }
                    //PiczelRoot result = JsonConvert.DeserializeObject<PiczelRoot>(jsonString.Substring(2, jsonString.Length - 3));
                }
                else
                {
                    await Context.Channel.SendMessageAsync("[CORE-ERR] Could not connect to the Piczel server.");
                }
            }
            catch (Exception e)
            {
                await Context.Channel.SendMessageAsync($"[CORE-ERR] Critical error: {e.Message}");
            }
        }

        [Command("watch")]
        public async Task registerStreamer(string username, string service)
        {
            Streamer streamerToAdd = new Streamer() { username = username, service = service };
            await Context.Channel.SendMessageAsync($"[DEBUG] Object created: Streamer.username: {streamerToAdd.username}, Streamer.service: {streamerToAdd.service}").ConfigureAwait(false);
        }

        private bool loadAPI(List<PiczelRoot> list)
        {
            try
            {
                piczelData = list;
                logger.LogCustom($"List has been loaded and contains {piczelData.Count} elements.", 0);
                return true;
            }
            catch (Exception e)
            {
                logger.LogCustom($"[CORE-ERR] {e.Message}", 1);
                return false;
            }
        }
    }
}