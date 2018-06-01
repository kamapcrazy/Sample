using System;
using System.Collections.Generic;
using System.Text;
using Xunit;

namespace Sample.PDF.Tests
{
    [CollectionDefinition("CustomWebServerFixtureCollection")]
    public class WebServerFixtureCollection : ICollectionFixture<ReportWebServerFixture>
    {
    }
}
