namespace WS.Views.Browser
{
    using Contracts;
    using Newtonsoft.Json;
    using SimpleInjector;
    using SimpleInjector.Diagnostics;
    using System;
    using System.Diagnostics;
    using System.Net;
    using WS.Remote;

    public class Program
    {
        public static void Main(string[] args)
        {
            var container = new Container();

            container.Register<IDeleteEnitity, DeleteEntityService>();
            container.Register<IGetEntities, GetEntitiesService>();
            container.Register<IPickEntities, PickEntitiesService>();
            container.Register<IZoomEntity, ZoomEntityService>();
            container.Register<IWatchDocument, WatchDocumentService>();

            container.Register<HttpListener>();
            container.Register<HttpJsonServer>();
            container.Register<ICanHandleRequest, RequestDispatcher>();

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

            var server = container
                .GetInstance<HttpJsonServer>();

            server.Start();

            var uri = new Uri($"http://127.0.0.1:8080");

            var pi = new ProcessStartInfo()
            {
                FileName = uri.AbsoluteUri,
                UseShellExecute = true
            };

            Process.Start(pi);

            Console.ReadKey();
            Console.ReadKey();

            server.Stop();
        }
    }
}
