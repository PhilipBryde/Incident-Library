using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class Archived : Page
    {
        public Archived()
        {
            InitializeComponent();
            LoadIncidentsAsync();
            // TODO: DataContext = new IncidentExplorerViewModel();
            // await ViewModel.LoadIncidentsByStatusAsync(3); // 3 = Archived
        }

        private async Task LoadIncidentsAsync()
        {
            var viewModel = new IncidentViewModel();
            var incidents = await viewModel.GetByStatusAsync(3);

            if (incidents.Count == 0)
            {
                txtEmpty.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                IncidentList.ItemsSource = incidents;
            }
        }
    }
}