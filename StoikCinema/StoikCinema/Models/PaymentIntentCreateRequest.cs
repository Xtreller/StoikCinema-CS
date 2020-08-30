using Newtonsoft.Json;

namespace StoikCinema.Models
{
    public class PaymentIntentCreateRequest
    {
        [JsonProperty("items")]
        public Item[] Items { get; set; }
    }
}
