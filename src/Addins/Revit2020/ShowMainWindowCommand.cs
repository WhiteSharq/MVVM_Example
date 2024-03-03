#define WPF
//#define WinForms

#if WPF
using WS.Views.WPF;
#endif

#if WinForms
using WS.Views.WinForms;
#endif

using Autodesk.Revit.Attributes;
using Autodesk.Revit.DB;
using Autodesk.Revit.UI;
using Contracts;
using SimpleInjector;
using WS.Models.Revit2020;
using WS.ViewModels;
using WS.ViewModels.Commands;

[Transaction(TransactionMode.Manual)]
public class ShowMainWindowCommand : IExternalCommand
{
    public Result Execute(
        ExternalCommandData commandData,
        ref string message,
        ElementSet elements)
    {
        var uiDocument = commandData
            .Application
            .ActiveUIDocument;

        var container = ConfigureUI();

        container.RegisterInstance(uiDocument);
        container.Register<IGetEntities, GetElementsService>();
        container.Register<IPickEntities, PickElementsService>();
        container.Register<IZoomEntity, ZoomElementService>();

        var window = container
            .GetInstance<MainWindow>();

        window.ShowDialog();

        return Result.Succeeded;
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
