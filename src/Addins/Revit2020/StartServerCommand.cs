#nullable enable

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Autodesk.Revit.UI.Events;
using Contracts;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using System.Net;
using WS.Models.Revit2020;
using WS.Remote;

[Transaction(TransactionMode.Manual)]
public class StartServerCommand : IExternalCommand
{
    private HttpJsonServer? _httpJsonServer;

    public Result Execute(
        ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var uiDocument = commandData
            .Application
            .ActiveUIDocument;

        var app = commandData
            .Application
            .Application;

        commandData.Application.ApplicationClosing +=
            On_Application_ApplicationClosing;

        var container = new Container();

        container.RegisterInstance(app);
        container.RegisterInstance(uiDocument);

        container.Register<IGetEntities, GetElementsService>();
        container.Register<IPickEntities, PickElementsService>();
        container.Register<IZoomEntity, ZoomElementService>();
        container.Register<IWatchDocument, WatchDocumentService>();
        container.Register<IDeleteEnitity, DeleteElementService>();
        container.Register<ElementToDTOConverter>();

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

        _httpJsonServer = container
            .GetInstance<HttpJsonServer>();

        _httpJsonServer.Start();

        return Result.Succeeded;
    }

    private void On_Application_ApplicationClosing(
        object sender,
        ApplicationClosingEventArgs e)
    {
        _httpJsonServer?.Stop();
    }
}