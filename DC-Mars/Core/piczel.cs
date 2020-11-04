using Newtonsoft.Json;
using System;
using System.Diagnostics;

namespace DC_Mars.Core
{
    public struct Piczel
    {
        [JsonProperty("id")]
        public int idStreamer { get; private set; }

        [JsonProperty("title")]
        public string title { get; private set; }

        [JsonProperty("description")]
        public string description { get; private set; }

        [JsonProperty("rendered_description")]
        public string rendered_description { get; private set; }

        [JsonProperty("follower_count")]
        public int follower_count { get; private set; }

        [JsonProperty("live")]
        public bool live { get; private set; }

        [JsonProperty("live_since")]
        public string live_since { get; private set; }

        [JsonProperty("slug")]
        public string slug { get; private set; }
    }
}