namespace WS.Models
{
    using Contracts;
    using NUnit.Framework;
    using SimpleInjector;
    using System;
    using System.Linq;

    public class UnitTests
    {
        IGetEntities GetEntitiesService { get; set; }

        [SetUp]
        public void Setup()
        {
            var container = new Container();

            container.Register<IDeleteEnitity, DeleteEntityService>();
            container.Register<IGetEntities, GetEntitiesService>();
            container.Register<IPickEntities, PickEntitiesService>();
            container.Register<IZoomEntity, ZoomEntityService>();
            container.Register<IWatchDocument, WatchDocumentService>();

            GetEntitiesService = container.GetInstance<IGetEntities>();
        }

        [TestCase("")]
        [TestCase("_")]
        [TestCase("fir")]
        public void GetEntities_FiltersByName(string? filter)
        {
            Assert.Throws<NullReferenceException>(
                () => GetEntitiesService.GetEntities(null));

            var entities = GetEntitiesService.GetEntities(filter);

            Assert.IsNotNull(entities);

            Assert.True(entities.All(e => e.Name.ToUpper().Contains(filter.ToUpper())));
        }

        public void GetEntities_ReturnsUniqueEntities()
        {
            var entities = GetEntitiesService.GetEntities("");

            if (entities.Length > 1)
            {
                var dups = entities
                    .GroupBy(e => e)
                    .Where(gr => gr.Skip(1).Any())
                    .ToArray();

                Assert.Zero(dups.Length);
            }
        }
    }
}