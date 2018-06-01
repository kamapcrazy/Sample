using System.IO;

namespace Sample.PDF.Services
{
    public interface IPdfService
    {
        Stream GenerateReport(string htmlContent);
    }
}
