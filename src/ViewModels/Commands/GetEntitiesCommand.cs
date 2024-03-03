namespace WS.ViewModels.Commands
{
    using Contracts;
    using System;
    using System.Windows.Input;

    public class GetEntitiesCommand : ICommand
    {
        private IGetEntities _getEntitiesService;

        public GetEntitiesCommand(
            IGetEntities getEntitesService)
        {
            _getEntitiesService = getEntitesService;
        }

        public event EventHandler? CanExecuteChanged;

        public event Action<EntityDTO[]> ResultObtained;

        public bool CanExecute(object parameter)
        {
            return
                parameter is string name &&
                !string.IsNullOrWhiteSpace(name)
                && name.Length > 2;
        }

        public void Execute(object parameter)
        {
            var entities = _getEntitiesService
                .GetEntities((string)parameter);

            ResultObtained.Invoke(entities);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(
                this,
                new EventArgs());
        }
    }
}