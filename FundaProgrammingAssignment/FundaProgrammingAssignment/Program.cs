using FundaContracts;
using System;
using System.Linq;

namespace FundaProgrammingAssignment
{
    class Program
    {
        // You may want to do this by dependency injection, but for this exercise I'll keep things simple
        // selecting the API as data supplier. Conceivable alternatives would be a DataBaseAdapter, or maybe a FileReader.
        private static readonly IOfferRetreiver offerRetriever = new FundaAPIAdapter();

        static void Main(string[] arg)
        {
            try
            {
                Process("offers in Amsterdam", "/amsterdam/");

                Console.WriteLine("");

                Process("offers in Amsterdam with a garden", "/amsterdam/tuin/");

            }
            catch (Exception ex)
            {
                Console.Write($"Exception {ex.Message}");
            }

            Console.ReadKey();
        }

        static void Process(string title, string query)
        {
            Console.WriteLine($"Retrieving {title}...");

            var offers = offerRetriever.GetOffers(query).ToList(); // converting to collection to avoid multiple enumerations

            if (offers.Count() == 0)
            {
                Console.WriteLine("No result found.");
                return;
            }

            Console.WriteLine($"{offers.Count()} offers found; ranking real estate agents...");
            Console.WriteLine($"Rank  Count   Real estate agent");

            int rank = 1;
            foreach (var offerAggregation in OfferAggregator.GetTopTenRealEstateAgents(offers))
            {
                Console.WriteLine($"{rank++,3}  {offerAggregation.Count,6}   {offerAggregation.RealEstateAgentName} (id={offerAggregation.RealEstateAgentId})");
            }
        }
    }
}
