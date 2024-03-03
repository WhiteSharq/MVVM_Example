namespace WS.ViewModels
{
    using Contracts;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using WS.ViewModels.Commands;

    public class MainViewModel : INotifyPropertyChanged
    {
        private EntityVM[] _entities;
        private string? _nameFilter;
        private EntityVM? _selectedEntity;

        public MainViewModel(
            GetEntitiesCommand getEntitiesCommand,
            PickEntitiesCommand pickEntitiesCommand,
            ZoomEntityCommand zoomEntitiyCommand)
        {
            GetEntitiesCommand = getEntitiesCommand;
            PickEntitiesCommand = pickEntitiesCommand;
            ZoomEntityCommand = zoomEntitiyCommand;

            Entities = new EntityVM[0];

            getEntitiesCommand.ResultObtained +=
                GetEntitiesCommand_ResultObtained;

            pickEntitiesCommand.ResultObtained +=
                PickEntitiesCommand_ResultObtained;
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public EntityVM[] Entities
        {
            get => _entities;
            set
            {
                _entities = value;
                OnPropertyChanged(nameof(Entities));
            }
        }

        public GetEntitiesCommand GetEntitiesCommand { get; }

        public string? NameFilter
        {
            get => _nameFilter;
            set
            {
                _nameFilter = value;
                OnPropertyChanged(nameof(NameFilter));
            }
        }

        public PickEntitiesCommand PickEntitiesCommand { get; }

        public EntityVM? SelectedEntity
        {
            get => _selectedEntity;
            set
            {
                _selectedEntity = value;
                OnPropertyChanged(nameof(SelectedEntity));
            }
        }

        public ZoomEntityCommand ZoomEntityCommand { get; }

        protected virtual void OnPropertyChanged(
            [CallerMemberName] string? propertyName = null)
        {
            PropertyChanged?.Invoke(
                this,
                new PropertyChangedEventArgs(propertyName));

            GetEntitiesCommand.RaiseCanExecuteChanged();
            PickEntitiesCommand.RaiseCanExecuteChanged();
            ZoomEntityCommand.RaiseCanExecuteChanged();
        }

        private void GetEntitiesCommand_ResultObtained(
            EntityDTO[] entities)
        {
            Entities = entities
                .Select(e => new EntityVM(e))
                .OrderBy(vm => vm.AsString)
                .ToArray();
        }

        private void PickEntitiesCommand_ResultObtained(EntityDTO[] entities)
        {
            Entities = entities
                .Select(e => new EntityVM(e))
                .OrderBy(vm => vm.AsString)
                .ToArray();
        }
    }
}