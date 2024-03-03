namespace WS.Models.Revit2020
{
    using Contracts;
    using NUnit.Framework;
    using SimpleInjector;

    public class RevitIntegrationTests : IntegrationTests
    {
        public RevitIntegrationTests()
        {
            var container = new Container();

            container.Register<IDeleteEnitity, DeleteElementService>();
            container.Register<IGetEntities, GetElementsService>();
            container.Register<IPickEntities, PickElementsService>();
            container.Register<IZoomEntity, ZoomElementService>();
            container.Register<IWatchDocument, WatchDocumentService>();

            DeleteEnitityService = container.GetInstance<IDeleteEnitity>();
            GetEntitiesService = container.GetInstance<IGetEntities>();
            PickEntitiesService = container.GetInstance<IPickEntities>();
            ZoomEntityService = container.GetInstance<IZoomEntity>();
            WatchDocumentService = container.GetInstance<IWatchDocument>();
        }

        [SetUp]
        public void Setup()
        {
        }

        // add some Revit specific tests
        [Test()]
        public void Some()
        {
        }
    }
}