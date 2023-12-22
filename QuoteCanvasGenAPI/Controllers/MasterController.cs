using Microsoft.AspNetCore.Mvc;
using QuoteCanvasGenAPI.Interfaces;
using QuoteCanvasGenAPI.Models;

namespace QuoteCanvasGenAPI.Controllers
{
    [Route("api")]
    [ApiController]
    public class MasterController : ControllerBase
    {
        private readonly ILogger<MasterController> _logger;
        private readonly IImageQuoteMergeService _imageQuoteMergeService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public MasterController(ILogger<MasterController> logger,
            IImageQuoteMergeService imageQuoteMergeService,
            IWebHostEnvironment webHostEnvironment)
        {
            _logger = logger;
            _imageQuoteMergeService = imageQuoteMergeService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [Route("generate")]
        public async Task<QuoteImageResponse> Get()
        {
            QuoteImageResponse reponse = await _imageQuoteMergeService.Generate();
            return reponse;
        }

        [HttpGet]
        [Route("image")]
        public IActionResult GetImageSync()
        {
            byte[] file_bytes = System.IO.File.ReadAllBytes(@"uploads/image.jpg");
            return File(file_bytes, "image/jpeg");
        }


    }
}
