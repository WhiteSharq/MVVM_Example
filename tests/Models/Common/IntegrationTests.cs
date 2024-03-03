namespace WS.Models
{
    using Contracts;
    using NUnit.Framework;
    using SimpleInjector;
    using System;
    using System.Linq;

    public class IntegrationTests
    {
        public IDeleteEnitity DeleteEnitityService { get; protected set; }
        public IGetEntities GetEntitiesService { get; protected set; }
        public IPickEntities PickEntitiesService { get; protected set; }
        public IZoomEntity ZoomEntityService { get; protected set; }
        public IWatchDocument WatchDocumentService { get; protected set; }

        [SetUp]
        public void Setup()
        {
            var container = new Container();

            container.Register<IDeleteEnitity, DeleteEntityService>();
            container.Register<IGetEntities, GetEntitiesService>();
            container.Register<IPickEntities, PickEntitiesService>();
            container.Register<IZoomEntity, ZoomEntityService>();
            container.Register<IWatchDocument, WatchDocumentService>();

            DeleteEnitityService = container.GetInstance<IDeleteEnitity>();
            GetEntitiesService = container.GetInstance<IGetEntities>();
            PickEntitiesService = container.GetInstance<IPickEntities>();
            ZoomEntityService = container.GetInstance<IZoomEntity>();
            WatchDocumentService = container.GetInstance<IWatchDocument>();
        }

        [Test()]
        public void GetEntities_DoesntReturnDeletedEntity()
        {
            var filter = "";

            var entities = GetEntitiesService.GetEntities(filter);

            Assert.IsNotNull(entities);

            Assert.IsNotEmpty(entities);

            var first = entities[0];

            Assert.DoesNotThrow(() => DeleteEnitityService.Delete(first));

            Assert.DoesNotThrow(() => entities = GetEntitiesService.GetEntities(filter));

            Assert.False(entities.Contains(first));
        }
    }
}