namespace WS.Views.WinForms
{
    using System;
    using System.Linq;
    using System.ComponentModel;
    using System.Windows.Forms;
    using WS.ViewModels;

    public partial class MainWindow : Form
    {
        private MainViewModel _viewModel;

        public MainWindow(MainViewModel viewModel)
        {
            _viewModel = viewModel;

            InitializeComponent();

            button1.Text = "Query";
            button2.Text = "Pick";
            button3.Text = "Zoom";

            button1.Click += Button1_Click;
            button2.Click += Button2_Click;
            button3.Click += Button3_Click;

            button1.Enabled = _viewModel
                .GetEntitiesCommand
                .CanExecute(_viewModel.NameFilter);


            textBox1.DataBindings.Add(
                "Text",
                _viewModel,
                nameof(_viewModel.NameFilter),
                false,
                DataSourceUpdateMode.OnPropertyChanged,
                "Enter");

            _viewModel.PropertyChanged += OnViewModel_PropertyChanged;
        }

        private void Button3_Click(object sender, EventArgs e)
        {
            var selectedItem = listView1
                .SelectedItems
                .Cast<ListViewItem>()
                .FirstOrDefault();

            _viewModel.ZoomEntityCommand.Execute(selectedItem);
        }

        private void Button2_Click(object sender, EventArgs e)
        {
            _viewModel.PickEntitiesCommand.Execute(null);
        }

        private void Button1_Click(
            object sender,
            EventArgs e)
        {
            _viewModel.GetEntitiesCommand.Execute(textBox1.Text);
        }

        private void OnViewModel_PropertyChanged(
            object sender,
            PropertyChangedEventArgs e)
        {
            var propName = e.PropertyName;

            button1.Enabled = _viewModel
                .GetEntitiesCommand
                .CanExecute(_viewModel.NameFilter);

            if (e.PropertyName == nameof(_viewModel.Entities))
            {
                listView1.Clear();

                // incorrect binding
                var vms = _viewModel
                    .Entities
                    .Select(vm =>
                        new ListViewItem()
                        {
                            Text = vm.AsString
                        })
                    .ToArray();

                listView1.Items.AddRange(vms);
            }
        }
    }
}
