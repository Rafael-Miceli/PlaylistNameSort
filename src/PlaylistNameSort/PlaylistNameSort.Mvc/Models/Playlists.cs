using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaylistNameSort.Mvc.Models
{
    public class Playlists
    {
        [JsonProperty("items")]
        public List<Playlist> Items { get; set; }
    }

    public class Playlist
    {        
        [JsonProperty("id")]
        public String Id { get; set; }
        [JsonProperty("name")]
        public String Name { get; set; }
        [JsonProperty("owner")]
        public PublicProfile Owner { get; set; }
    }
}