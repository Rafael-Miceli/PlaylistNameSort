using Newtonsoft.Json;
using System;

namespace PlaylistNameSort.Domain.Models
{
    public class PublicProfile
    {
        [JsonProperty("id")]
        public String Id { get; set; }
    }
}