using FundaContracts;
using System.Collections.Generic;
using System.Linq;

namespace FundaProgrammingAssignment
{
    public class OfferAggregator
    {
        public static IEnumerable<OfferAggregationByAgent> GetTopTenRealEstateAgents(IEnumerable<Offer> offers)
        {
            // According to the assignment: 
            // "Determine which makelaar's [sic] in Amsterdam have the most object listed for sale. Make a table of the top 10."
            return offers
                .GroupBy(offer => offer.RealEstateAgentId)
                .Select(group => new OfferAggregationByAgent
                {
                    RealEstateAgentId = group.First().RealEstateAgentId,
                    RealEstateAgentName = group.First().RealEstateAgentName,
                    Count = group.Count()
                })
                .OrderByDescending(group => group.Count)
                .Take(10);
        }
    }
}
