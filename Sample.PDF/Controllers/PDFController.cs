using Microsoft.AspNetCore.Mvc;
using Sample.PDF.Extensions;
using Sample.PDF.Services;
using System.Threading.Tasks;

namespace Sample.PDF.Controllers
{
    [Route("api/[controller]")]
    public class PdfController : Controller
    {
        private const string ContentType = "application/pdf";
        private readonly IPdfService _pdfService;

        public PdfController(IPdfService pdfService)
        {
            _pdfService = pdfService;
        }

        [HttpGet]
        public async Task<IActionResult> GetReport()
        {
            await ViewTemplate();

            var htmlContent = await View("ViewTemplate").ToHtml(HttpContext);

            var reportData = _pdfService.GenerateReport(htmlContent);

            return File(reportData, ContentType, "sample_report.pdf");
        }

        private async Task<ViewResult> ViewTemplate()
        {
            await Task.Delay(1000);

            return View();
        }
    }
}