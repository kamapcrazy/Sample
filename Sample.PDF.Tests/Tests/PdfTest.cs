using System.Net.Http;
using System.Threading.Tasks;
using Xunit;

namespace Sample.PDF.Tests.Tests
{
    [Collection("CustomWebServerFixtureCollection")]
    public class PdfTest
    {
        public PdfTest(ReportWebServerFixture fixture)
        {
            _client = fixture.Client;
        }

        private readonly HttpClient _client;

        [Fact]
        public async Task ShouldReturnReportForCatalogItem()
        {
            var fileResult =
                await _client.GetAsync($"api/pdf");

            Assert.NotEqual(0, fileResult.Content.Headers.ContentLength);
            Assert.True(fileResult.Content.Headers.ContentLength > 0);
            Assert.True(fileResult.IsSuccessStatusCode);
            Assert.Equal("application/pdf", fileResult.Content.Headers.ContentType.ToString());
        }
    }
}
