namespace WS.ViewModels.Commands
{
    using Contracts;
    using System;
    using System.Windows.Input;
    using WS.ViewModels;

    public class PickEntitiesCommand : ICommand
    {
        private bool _documentIsActive;
        private IPickEntities _pickEntitiesService;
        private IWatchDocument _watchDocumentService;
        private bool _watchServiceStarted;

        public PickEntitiesCommand(
            IPickEntities pickEntitiesService,
            IWatchDocument watchDocumentService)
        {
            _pickEntitiesService = pickEntitiesService;
            
            watchDocumentService.Activated +=
                OnWatchDocumentService_Activated;

            _watchDocumentService = watchDocumentService;
        }

        public event EventHandler? CanExecuteChanged;

        public event Action<EntityDTO[]> ResultObtained;

        public bool CanExecute(object parameter)
        {
            if (!_watchServiceStarted)
            {
                _watchDocumentService.Start();

                _watchServiceStarted = true;
            }

            var vm = (MainViewModel?)parameter;

            return _documentIsActive;
        }

        public void Execute(object parameter)
        {
            var res = _pickEntitiesService
                .Select(null);

            ResultObtained?.Invoke(res);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(
                this,
                new EventArgs());
        }

        private void OnWatchDocumentService_Activated(bool active)
        {
            _documentIsActive = active;

            //RaiseCanExecuteChanged();
        }
    }
}