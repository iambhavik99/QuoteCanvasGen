using QuoteCanvasGenAPI.Interfaces;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;
using SixLabors.Fonts;
using SixLabors.ImageSharp.Drawing;
using Microsoft.AspNetCore.Hosting.Server;
using Microsoft.AspNetCore.Hosting.Server.Features;
using QuoteCanvasGenAPI.Models;

namespace QuoteCanvasGenAPI.Services
{
    public class ImageQuoteMergeService : IImageQuoteMergeService
    {
        private readonly IImageProviderService _imageProviderService;
        private readonly IQuoteProviderService _quoteProviderService;
        private readonly IServer _server;
        public ImageQuoteMergeService(
            IImageProviderService imageProviderService,
            IQuoteProviderService quoteProviderService,
            IServer server
            )
        {
            _imageProviderService = imageProviderService;
            _quoteProviderService = quoteProviderService;
            _server = server;
        }
        public void Merge(byte[] imageBytes, string quote, string authorName)
        {

            // Load the downloaded image
            using (var image = Image.Load<Rgba32>(imageBytes))
            {
                // Add font family
                FontCollection collection = new();
                FontFamily family = collection.Add("Assets\\RedHatDisplay-ExtraBold.ttf");
                Font font = family.CreateFont(42, FontStyle.Regular);
                var brush = new SolidBrush(Color.Black);

                var center = new PointF(image.Width / 2, image.Height / 2);
                var textOptions = new RichTextOptions(font)
                {
                    Origin = center,
                    HorizontalAlignment = HorizontalAlignment.Center,
                    VerticalAlignment = VerticalAlignment.Center,
                    WrappingLength = image.Width - 20
                };

                quote += "\n\t - " + authorName;

                // Add text to the image
                image.Mutate(ctx => ctx.DrawText(textOptions, quote, brush));

                string path = "uploads";
                bool exists = Directory.Exists(path);

                if (!exists)
                    Directory.CreateDirectory(path);

                // Save the modified image to disk
                string outputPath = "uploads/image.jpg";
                image.Save(outputPath);

            }
        }
        public async Task<QuoteImageResponse> Generate()
        {
            try
            {
                QuoteImageResponse quoteImageResponse = new QuoteImageResponse();

                // fetch image from cloud
                string url = await _imageProviderService.GetImageUrl();
                url = url.Replace("w=1080", "h=720");

                // convert it into bytes
                byte[] imageBytes = await _imageProviderService.GetImageBytes(url);

                // fetch quote details
                QuoteResponse quoteResponse = await _quoteProviderService.GetQuoteDetails();

                // fetch quote author details
                QuoteAuthorResponse quoteAuthorResponse = await _quoteProviderService.GetQuoteAuthorDetails(quoteResponse.authorSlug);

                // merge quote text and auther name to the image
                Merge(imageBytes, quoteResponse.content, quoteResponse.author);

                quoteImageResponse.quote = quoteResponse.content;
                quoteImageResponse.imageUrl = _server.Features.Get<IServerAddressesFeature>().Addresses.First() + "/api/image";
                quoteImageResponse.authorName = quoteResponse.author;
                quoteImageResponse.authorDescription = quoteAuthorResponse.description;
                quoteImageResponse.authorBio = quoteAuthorResponse.bio;
                quoteImageResponse.authorLink = quoteAuthorResponse.link;

                return quoteImageResponse;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
                return new QuoteImageResponse();
            }

        }
    }
}
