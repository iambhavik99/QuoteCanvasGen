using QuoteCanvasGenAPI.Interfaces;
using QuoteCanvasGenAPI.Models;
using RestSharp;

namespace QuoteCanvasGenAPI.Services
{
    public class ImageProviderService : IImageProviderService
    {
        private readonly IConfiguration _configuration;

        public ImageProviderService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GetImageUrl()
        {
            var accessKey = _configuration.GetValue<string>("QuoteAPISecrets:AccessKey");
            var options = new RestClientOptions("https://api.unsplash.com")
            {
                MaxTimeout = -1,
            };

            var client = new RestClient(options);
            var request = new RestRequest("/photos/random?query=minimalistic&orientation=landscape", Method.Get);
            request.AddHeader("Accept-Version", "v1");
            request.AddHeader("Authorization", $"Client-ID {accessKey}");
            RestResponse<ImageResponse> response = await client.ExecuteAsync<ImageResponse>(request);

            if (response.IsSuccessful)
            {
                return response?.Data?.urls.regular;
            }

            return String.Empty;
        }

        public async Task<byte[]> GetImageBytes(string imageUrl)
        {
            byte[] imageData = new byte[] { };
            var client = new RestClient(imageUrl);
            var request = new RestRequest("", Method.Get);
            RestResponse response = await client.ExecuteAsync(request);

            if (response.IsSuccessful)
            {
                imageData = response.RawBytes; // Raw bytes of the downloaded image
            }

            return imageData;
        }


    }
}
