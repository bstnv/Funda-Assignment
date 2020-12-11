using System.Collections.Generic;
using Newtonsoft.Json;

namespace FundaContracts
{
    public class Offer
    // For this exercise, we're only interested in the real estate agent data from the offer.
    // Formally, the Json attributes should not be in the contracts and be moved to the OfferApiAdapter as they are specific to the REST API. However, I might be over-engeneering already by making a separate contract layer.
    {
        [JsonProperty("MakelaarId")]
        public int RealEstateAgentId { get; set; }

        [JsonProperty("MakelaarNaam")]
        public string RealEstateAgentName { get; set; }
    }

    public class Root
    {
        [JsonProperty("Objects")]
        public List<Offer> Offers { get; set; }
    }
}
