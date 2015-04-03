using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PlaylistNameSort.Mvc.Models
{
    public class SpotifyUser
    {
        [JsonProperty("id")]
        public string UserId { get; set; }
        [JsonProperty("display_name")]
        public String DisplayName { get; set; }
    }
}