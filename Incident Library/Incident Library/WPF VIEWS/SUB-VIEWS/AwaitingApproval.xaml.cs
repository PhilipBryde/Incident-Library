using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class AwaitingApproval : Page
    {
        private readonly IncidentViewModel _vm = new IncidentViewModel();

        public AwaitingApproval()
        {
            InitializeComponent();
            Loaded += async (s, e) => await LoadIncidentsAsync();
        }

        private async System.Threading.Tasks.Task LoadIncidentsAsync()
        {
            List<IncidentReport> incidents = await _vm.GetByStatusAsync(4);
            if (incidents.Count == 0)
            {
                txtEmpty.Visibility = Visibility.Visible;
                IncidentList.Visibility = Visibility.Collapsed;
            }
            else
            {
                txtEmpty.Visibility = Visibility.Collapsed;
                IncidentList.Visibility = Visibility.Visible;
                IncidentList.ItemsSource = incidents;
            }
        }

        private void IncidentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IncidentList.SelectedItem is IncidentReport selected)
            {
                NavigationService?.Navigate(new EditIncidentReport(selected));
            }
        }
    }
}