namespace WS.ViewModel
{
    using Contracts;
    using NUnit.Framework;
    using SimpleInjector;
    using WS.ViewModels.Commands;
    using WS.ViewModels;
    using System.Linq;

    public class ViewModelTests
    {
        Container _container;

        [SetUp]
        public void Setup()
        {
            var container = new Container();


            container.Register<GetEntitiesCommand>();
            container.Register<PickEntitiesCommand>();
            container.Register<ZoomEntityCommand>();
            container.Register<MainViewModel>();

            container.Register<IDeleteEnitity, DeleteEntityService>();
            container.Register<IGetEntities, GetEntitiesService>();
            container.Register<IPickEntities, PickEntitiesService>();
            container.Register<IZoomEntity, ZoomEntityService>();
            container.Register<IWatchDocument, WatchDocumentService>();

            _container = container;
        }

        [TestCase("")]
        [TestCase("11")]
        [TestCase("fi")]
        public void GetEntitiesCommand_CannotExecute(string filter)
        {
            var vm = _container
                .GetInstance<MainViewModel>();

            Assert.False(vm.GetEntitiesCommand.CanExecute(filter));
        }

        [TestCase("123")]
        [TestCase("fir")]
        [TestCase("second")]
        public void GetEntitiesCommand_CanExecute(string filter)
        {
            var vm = _container
                .GetInstance<MainViewModel>();

            Assert.True(vm.GetEntitiesCommand.CanExecute(filter));
        }

        public void ZoomEntityCommand_CanExecute_IfEntitySelected()
        {
            var vm = _container
                .GetInstance<MainViewModel>();

            Assert.That(vm.ZoomEntityCommand.CanExecute(null), Is.EqualTo(vm.SelectedEntity is not null));
        }

        public void EntitiesList_IsOrdered()
        {
            var vm = _container
                .GetInstance<MainViewModel>();

            vm.GetEntitiesCommand.Execute("");

            if (vm.Entities.Length > 1)
            {
                var ordered = vm.Entities
                    .OrderBy(e => e)
                    .ToArray();

                ordered.Zip(vm.Entities, (o, vm) => (o, vm))
                    .ToList()
                    .ForEach(t => Assert.That(t.o, Is.SameAs(t.vm)));
            }
        }

        public void EntitiesList_HasNoDups()
        {
            var vm = _container
                .GetInstance<MainViewModel>();

            vm.GetEntitiesCommand.Execute("");

            if (vm.Entities.Length > 1)
            {
                var dups = vm.Entities
                    .GroupBy(e => e)
                    .Where(gr => gr.Skip(1).Any())
                    .ToArray();

                Assert.Zero(dups.Length);
            }
        }
    }
}