using Newtonsoft.Json;
using System;

namespace PlaylistNameSort.Mvc.Models
{
    public class PublicProfile
    {
        [JsonProperty("id")]
        public String Id { get; set; }
    }
}