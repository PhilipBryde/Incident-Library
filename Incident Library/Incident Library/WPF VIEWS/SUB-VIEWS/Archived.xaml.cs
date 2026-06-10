using Incident_Library.SORTING;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows.Controls;
using System.Windows;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class Archived : Page
    {
        private IncidentViewModel _viewModel = new IncidentViewModel();

        public Archived()
        {
            InitializeComponent();
            SortDropdown.SelectedIndex = 0;
            LoadIncidentsAsync();
        }

        private async void LoadIncidentsAsync()
        {
            var incidents = await _viewModel.GetByStatusAsync(3);
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