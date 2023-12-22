namespace QuoteCanvasGenAPI.Interfaces
{
    public interface IImageProviderService
    {
        Task<string> GetImageUrl();
        Task<byte[]> GetImageBytes(string imageUrl);
    }
}
