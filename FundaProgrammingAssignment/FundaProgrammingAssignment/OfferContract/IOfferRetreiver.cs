using System.Collections.Generic;

namespace FundaContracts
{
    public interface IOfferRetreiver
    // Contract between producers and consumers of offer data.
    {
        // Arguably, if you plan to implement alternative retrievers, you may want to refactor the query string into something more generic
        // E.g. a separate DTO containing a string for area name (or perhaps an enum) and a bool for 'hasGarden'.
        IEnumerable<Offer> GetOffers(string query);
    }
}
