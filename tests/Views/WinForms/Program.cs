namespace WS.WinForms
{
    using WS.Views.WinForms;
    using System;
    using System.Windows.Forms;
    using SimpleInjector;
    using Contracts;
    using WS.ViewModels.Commands;
    using WS.ViewModels;
    using SimpleInjector.Diagnostics;

    public static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var container = new Container();

            container.Register<GetEntitiesCommand>();
            container.Register<PickEntitiesCommand>();
            container.Register<ZoomEntityCommand>();
            container.Register<MainViewModel>();
            container.Register<MainWindow>();

            container.Register<IGetEntities, GetEntitiesService>();
            container.Register<IPickEntities, PickEntitiesService>();
            container.Register<IZoomEntity, ZoomEntityService>();
            container.Register<IWatchDocument, WatchDocumentService>();

            var registration = container
                .GetRegistration(typeof(MainWindow))
                .Registration;

            registration.SuppressDiagnosticWarning(
                DiagnosticType.DisposableTransientComponent,
                "Reason of suppression");

            var window = container
                .GetInstance<MainWindow>();

            Application.Run(window);
        }
    }
}
