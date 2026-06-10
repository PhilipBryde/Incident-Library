using Incident_Library.MODELS__Data_;
using System.Windows;
using System.Windows.Controls;
using Incident_Library.SORTING;
using Incident_Library.VIEWMODELS_LOGIC_;


namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class UnderReview : Page
    {
        private IncidentViewModel _viewModel = new IncidentViewModel();
        public UnderReview()
        {
            InitializeComponent();
            SortDropdown.SelectedIndex = 0;
            LoadIncidentsAsync();
            // TODO: DataContext = new IncidentExplorerViewModel();
            // await ViewModel.LoadIncidentsByStatusAsync(2); // 2 = Under Review     
        }

        private async void LoadIncidentsAsync()
        {
            var incidents = await _viewModel.GetByStatusAsync(2);
            if (incidents.Count == 0)
            {
                txtEmpty.Visibility = Visibility.Visible;
            }
            else
            {
                txtEmpty.Visibility = Visibility.Collapsed;
                IncidentList.ItemsSource = incidents;
            }
        }

        private void SortDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortDropdown.SelectedIndex == 0)
                _viewModel.SetSortStrategy(new SortbyDateNewest());
            else
                _viewModel.SetSortStrategy(new SortByDateOldest());

            LoadIncidentsAsync();
        }

    }
}