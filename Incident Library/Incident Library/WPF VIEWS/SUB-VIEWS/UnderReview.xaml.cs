using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class UnderReview : Page
    {
        public UnderReview()
        {
            InitializeComponent();
            LoadIncidentsAsync();
            // TODO: DataContext = new IncidentExplorerViewModel();
            // await ViewModel.LoadIncidentsByStatusAsync(2); // 2 = Under Review     
        }

       private async Task LoadIncidentsAsync()
        {
            var viewModel = new IncidentViewModel();
            var incidents = await viewModel.GetByStatusAsync(2);

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