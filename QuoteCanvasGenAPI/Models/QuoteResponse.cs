namespace QuoteCanvasGenAPI.Models
{
    public class QuoteResponse
    {
        public string content { get; set; }
        public string author { get; set; }
        public string authorSlug { get; set; }
    }

    public class QuoteAuthorResponse
    {
        public string name { get; set; }
        public string bio { get; set; }
        public string description { get; set; }
        public string link { get; set; }
    }
}
