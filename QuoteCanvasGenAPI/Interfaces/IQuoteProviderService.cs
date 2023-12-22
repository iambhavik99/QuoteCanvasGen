using QuoteCanvasGenAPI.Models;

namespace QuoteCanvasGenAPI.Interfaces
{
    public interface IQuoteProviderService
    {
        Task<QuoteResponse> GetQuoteDetails();
        Task<QuoteAuthorResponse> GetQuoteAuthorDetails(string authorSlug);

    }
}
