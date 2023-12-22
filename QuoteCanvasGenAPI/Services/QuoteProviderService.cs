using QuoteCanvasGenAPI.Interfaces;
using QuoteCanvasGenAPI.Models;
using RestSharp;

namespace QuoteCanvasGenAPI.Services
{
    public class QuoteProviderService : IQuoteProviderService
    {
        public async Task<QuoteResponse> GetQuoteDetails()
        {
            var options = new RestClientOptions("https://api.quotable.io")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/random", Method.Get);
            RestResponse<QuoteResponse> response = await client.ExecuteAsync<QuoteResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return new QuoteResponse();
        }

        public async Task<QuoteAuthorResponse> GetQuoteAuthorDetails(string authorSlug)
        {
            var options = new RestClientOptions("https://api.quotable.io")
            {
                MaxTimeout = -1,
            };
            var client = new RestClient(options);
            var request = new RestRequest("/authors/slug/" + authorSlug, Method.Get);
            RestResponse<QuoteAuthorResponse> response = await client.ExecuteAsync<QuoteAuthorResponse>(request);

            if (response.IsSuccessful)
            {
                return response.Data;
            }

            return new QuoteAuthorResponse();
        }
    }
}
