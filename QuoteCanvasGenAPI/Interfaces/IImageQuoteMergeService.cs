using QuoteCanvasGenAPI.Models;

namespace QuoteCanvasGenAPI.Interfaces
{
    public interface IImageQuoteMergeService
    {
        public void Merge(byte[] imageBytes,string quote, string authorName);
        public Task<QuoteImageResponse> Generate();
    }
}
