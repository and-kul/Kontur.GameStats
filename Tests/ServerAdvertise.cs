using System.IO;
using Kontur.GameStats.Server;
using Kontur.GameStats.Server.Data.Persistence;
using Kontur.GameStats.Server.Info;
using Kontur.GameStats.Server.Nancy;
using Nancy;
using Nancy.Bootstrapper;
using Nancy.Testing;
using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;
using NUnit.Framework;

namespace Tests
{
    [TestFixture]
    public class ServerAdvertise
    {
        private INancyBootstrapper bootstrapper;
        private Browser browser;

        [OneTimeSetUp]
        public void OneTimeSetUp()
        {
            File.Delete(Path.Combine(System.AppDomain.CurrentDomain.BaseDirectory, "temp.sqlite"));
            GameStatsDbContext.UseTestConnectionString = true;

            var jsonSettings = new JsonSerializerSettings
            {
                ContractResolver = new CamelCasePropertyNamesContractResolver(),
                DateFormatHandling = DateFormatHandling.IsoDateFormat,
                DateTimeZoneHandling = DateTimeZoneHandling.RoundtripKind,
                Formatting = Formatting.Indented
            };

            JsonConvert.DefaultSettings = () => jsonSettings;

            bootstrapper = new Bootstrapper();
            browser = new Browser(bootstrapper);
        }


        [Test]
        public void AddNewServer()
        {
            var endpoint = "1.1.1.1-1234";
            var advertise = new
            {
                Name = "] My P3rfect Server [",
                GameModes = new[] {"DM", "TDM"}
            };

            var json = JsonConvert.SerializeObject(advertise);

            var putResult = browser.Put($"/servers/{endpoint}/info", with => { with.Body(json); });

            Assert.That(putResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var getResult = browser.Get($"/servers/{endpoint}/info");

            Assert.That(getResult.StatusCode, Is.EqualTo(HttpStatusCode.OK));

            var resultServerInfo = JsonConvert.DeserializeObject<ServerInfo>(getResult.Body.AsString());

            Assert.That(resultServerInfo.Name, Is.EqualTo(advertise.Name));
            Assert.That(resultServerInfo.GameModes, Is.EquivalentTo(advertise.GameModes));
        }

        [OneTimeTearDown]
        public void OneTimeTearDown()
        {
            GameStatsDbContext.UseTestConnectionString = false;
        }
    }
}