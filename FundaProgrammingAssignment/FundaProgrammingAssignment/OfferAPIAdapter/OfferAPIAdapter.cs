using Flurl;
using Flurl.Http;
using FundaContracts;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Threading;

namespace FundaProgrammingAssignment
{
    public class FundaAPIAdapter : IOfferRetreiver
    // implements IOfferRetreiver by using the REST API Aanbod.svc
    {
        private readonly string baseUrl = "http://partnerapi.funda.nl/feeds/Aanbod.svc/json";
        private const int pageSize = 100; // a rather arbitrary number. The API seems to be reverting to 25 anyway.

        private async Task<Root> RetrievePage(int pageNum, string query, bool allow401)
        {
            var url = baseUrl
                      .AppendPathSegment("ac1b0b1572524640a0ecc54de453ea9f")
                      .SetQueryParams(new
                      {
                          type = "koop",
                          zo = query,
                          page = pageNum,
                          pagesize = pageSize
                      }
                      );

            try
            {
                return await url.GetJsonAsync<Root>();
            }
            catch (FlurlHttpException ex)
            {
                if (ex.Call.Response.StatusCode == 401 && allow401) {
                    // We're getting a 401. Likely due to overloading the API.
                    // Give it some rest and try again, once.
                    Console.WriteLine($"401 returned at page {pageNum}. Retrying in a minute...");
                    Thread.Sleep(TimeSpan.FromSeconds(61));
                    return await RetrievePage(pageNum, query, false);
                }

                // Assignment is unclear on HOW to handle other errors, so I'll just dump some data and return null
                Console.WriteLine($"error={ex.Call.Response.StatusCode}");
                Console.WriteLine($"message={ex.Message}");
                Console.WriteLine($"url={url}");
                return null;
            }
        }

        public IEnumerable<Offer> GetOffers(string query)
        {
            bool done = false;
            int pageNum = 1;
            while (!done)
            {
                var offers = RetrievePage(pageNum, query, true).Result?.Offers;
                done = offers == null || offers.Count == 0;

                if (!done)
                    foreach (var offer in offers)
                        yield return offer;

                pageNum++;
            }
            Console.WriteLine($"pages = {pageNum-1}");
        }
    }
}
