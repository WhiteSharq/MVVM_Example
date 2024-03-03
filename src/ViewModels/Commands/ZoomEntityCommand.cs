namespace WS.ViewModels.Commands
{
    using Contracts;
    using System;
    using System.Windows.Input;
    using WS.ViewModels;

    public class ZoomEntityCommand : ICommand
    {
        private IZoomEntity _zoomEntityService;

        public ZoomEntityCommand(
            IZoomEntity zoomEntityService)
        {
            _zoomEntityService = zoomEntityService;
        }

        public event EventHandler? CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            var vm = (MainViewModel?)parameter;

            return vm?.SelectedEntity is not null;
        }

        public void Execute(object parameter)
        {
            var vm = (MainViewModel)parameter;

            if (vm?.SelectedEntity is null)
            {
                throw new ArgumentNullException(
                    nameof(MainViewModel.SelectedEntity),
                    $"Check {nameof(ZoomEntityCommand)}" +
                    $"{nameof(CanExecute)}");
            }

            _zoomEntityService.Zoom(
                vm.SelectedEntity.DTO);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(
                this,
                new EventArgs());
        }
    }
}