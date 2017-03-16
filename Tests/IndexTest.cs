using Kontur.GameStats.Server;
using Kontur.GameStats.Server.Info;
using Nancy;
using Nancy.Testing;
using Nancy.ViewEngines;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class IndexTest
    {
      
        [Test]
        public void TestIndexPage()
        {
            // Given
            var bootstrapper = new Bootstrapper();
            var browser = new Browser(bootstrapper);

            // When
            var result = browser.Get("/", with =>
            {
                with.HttpRequest();
            });

            // Then
            Assert.That(result.StatusCode, Is.EqualTo(HttpStatusCode.OK));
            Assert.That(result.Body.AsString(), Does.StartWith("Hello"));
            
        }



    }
}
