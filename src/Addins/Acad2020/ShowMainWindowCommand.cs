//#define WPF
#define WinForms

#if WPF
using WS.Views.WPF;
#endif

#if WinForms
using WS.Views.WinForms;
#endif

using Autodesk.AutoCAD.DatabaseServices.IoC;
using Autodesk.AutoCAD.EditorInput.IoC;
using Autodesk.AutoCAD.Runtime;
using Contracts;
using SimpleInjector;
using SimpleInjector.Diagnostics;
using WS.Models.Acad2020;
using WS.ViewModels;
using WS.ViewModels.Commands;



[assembly:
    Autodesk.AutoCAD.Runtime.CommandClass(
    typeof(ShowMainWindowCommand))]

public class ShowMainWindowCommand
{

    [CommandMethod(
        "wsmw",
        CommandFlags.Redraw |
        CommandFlags.Modal |
        CommandFlags.UsePickSet |
        CommandFlags.NoPaperSpace |
        CommandFlags.NoBlockEditor)]
    public void ShowMainWindow()
    {
        var container = ConfigureUI();

        container.Register<IGetEntities, GetEntitiesService>();
        container.Register<IPickEntities, PickEntitiesService>();
        container.Register<IZoomEntity, ZoomEntityService>();
        container.Register<IWatchDocument, WatchDocumentService>();

        container.Register<IEditor, EditorImpl>();
        container.Register<IDatabase, DatabaseImpl>();
        container.Register<EntityToDTOConverter>();

        var registration = container
            .GetRegistration(typeof(MainWindow))
            .Registration;

        registration.SuppressDiagnosticWarning(
            DiagnosticType.DisposableTransientComponent,
            "Reason of suppression");

        var window = container
            .GetInstance<MainWindow>();

        window.ShowDialog();
    }

    public static Container ConfigureUI()
    {
        var container = new Container();
        container.Register<GetEntitiesCommand>();
        container.Register<PickEntitiesCommand>();
        container.Register<ZoomEntityCommand>();
        container.Register<MainViewModel>();
        container.Register<MainWindow>();
        return container;
    }
}