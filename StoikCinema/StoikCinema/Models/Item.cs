using Newtonsoft.Json;

namespace StoikCinema.Models
{
    public class Item
    {
        [JsonProperty("id")]
        public string Id { get; set; }
    }
}
