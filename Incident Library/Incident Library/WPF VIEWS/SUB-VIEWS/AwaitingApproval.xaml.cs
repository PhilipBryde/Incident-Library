using Incident_Library.SORTING;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class AwaitingApproval : Page
    {
        private IncidentViewModel _viewModel = new IncidentViewModel();
        public AwaitingApproval()
        {
            InitializeComponent();
            SortDropdown.SelectedIndex = 0;
            LoadIncidentsAsync();
            // TODO: DataContext = new IncidentExplorerViewModel();
            // await ViewModel.LoadIncidentsByStatusAsync(4); // 4 = Awaiting Approval
        }

        private async void LoadIncidentsAsync()
        {
            var incidents = await _viewModel.GetByStatusAsync(4);
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

        private void BtnApprove_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                // TODO: await ViewModel.ApproveIncidentAsync(id);
                // Moves incident to Archived status
            }
        }

        private void BtnReject_Click(object sender, RoutedEventArgs e)
        {
            if (sender is Button btn && btn.Tag is int id)
            {
                // TODO: await ViewModel.RejectIncidentAsync(id);
                // Moves incident back to Work In Progress
            }
        }
    }
}