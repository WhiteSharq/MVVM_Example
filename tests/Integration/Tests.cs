namespace Test.WS.Integration
{
    using Contracts;
    using global::WS.Remote;
    using NUnit.Framework;
    using SimpleInjector;
    using SimpleInjector.Diagnostics;
    using System.Net;
    using System.Net.Http;
    using System.Net.Http.Json;

    public class Tests
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
            //container.Register<ICanHandleRequest, RequestDispatcher>();

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
            var requestDTO = new EnititesRequestDTO(namePart);

            var request = _httpClient.PostAsJsonAsync("", requestDTO).Result;

            Assert.Equals(request.StatusCode, HttpStatusCode.OK);
        }

        [TearDown]
        public void TearDown()
        {
            _server.Stop();
        }
    }
}