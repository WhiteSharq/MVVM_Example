namespace WS.Remote
{
    using Contracts;
    using NUnit.Framework;
    using SimpleInjector;
    using SimpleInjector.Diagnostics;
    using System.Net;
    using System.Net.Http;
    //using System.Net.Http.Json;

    public class HttpJsonServerTests
    {
        private HttpJsonServer _server;
        private HttpClient _httpClient;

        [SetUp]
        public void Setup()
        {
            var container = new Container();

            container.RegisterInstance(new HttpListener());
            container.Register<HttpJsonServer, HttpJsonServer>();
            container.Register<ICanHandleRequest>();

            var registration = container
                .GetRegistration(typeof(HttpListener))
                .Registration;

            registration.SuppressDiagnosticWarning(
                DiagnosticType.DisposableTransientComponent,
                "Reason of suppression");

            registration = container
                .GetRegistration(typeof(HttpJsonServer))
                .Registration;

            registration.SuppressDiagnosticWarning(
                DiagnosticType.DisposableTransientComponent,
                "Reason of suppression");

            _server = container.GetInstance<HttpJsonServer>();

            _server.Start();
        }

        [TestCase("sdfsdf")]
        public void Test1(string namePart)
        {
            var request = _httpClient.PostAsync("", null).Result;

            Assert.Equals(request.StatusCode, HttpStatusCode.OK);
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }
    }
}