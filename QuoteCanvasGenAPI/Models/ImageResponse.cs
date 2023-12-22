namespace QuoteCanvasGenAPI.Models
{
    public class ImageResponse
    {
        public string id { get; set; }
        public ImageURL urls { get; set; }

    }

    public class ImageURL
    {
        public string regular { get; set; }
        public string full { get; set; }
    }
}
