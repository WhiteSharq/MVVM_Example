namespace WS.Remote
{
    using Contracts;
    using Newtonsoft.Json;
    using NUnit.Framework;
    using SimpleInjector;

    internal class RequestDispatcherTests
    {
        private ICanHandleRequest _requestDispatcher;

        [SetUp]
        public void Setup()
        {
            var container = new Container();

            container.Register<IDeleteEnitity, DeleteEntityService>();
            container.Register<IGetEntities, GetEntitiesService>();
            container.Register<IPickEntities, PickEntitiesService>();
            container.Register<IZoomEntity, ZoomEntityService>();
            container.Register<IWatchDocument, WatchDocumentService>();
            container.Register<ICanHandleRequest, RequestDispatcher>();

            _requestDispatcher = container.GetInstance<ICanHandleRequest>();
        }

        [TestCase("")]
        [TestCase("sd")]
        [TestCase("sdfsdf")]
        [TestCase("firs")]
        public void GetEntitiesRequest(string namePart)
        {
            var requestDTO = new EnititesRequestDTO(namePart);

            var requestJson = JsonConvert
                .SerializeObject(requestDTO);

            object? resp = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                resp = await _requestDispatcher
                    .Handle(requestJson);
            });

            Assert.IsInstanceOf<EntityDTO[]>(resp);
        }

        public void PickEntitiesRequest()
        {
            var requestDTO = new PickRequestDTO(new object());

            var requestJson = JsonConvert
                .SerializeObject(requestDTO);

            object? resp = null;

            Assert.DoesNotThrowAsync(async () =>
            {
                resp = await _requestDispatcher
                    .Handle(requestJson);
            });

            Assert.IsInstanceOf<EntityDTO>(resp);
        }
    }
}