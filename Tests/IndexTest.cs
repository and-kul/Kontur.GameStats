﻿using Kontur.GameStats.Server.Nancy;
using Nancy;
using Nancy.Testing;
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
