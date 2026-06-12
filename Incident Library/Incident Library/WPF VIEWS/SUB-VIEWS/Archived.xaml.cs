using Incident_Library.MODELS__Data_;
using Incident_Library.SORTING;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class Archived : Page
    {
        private readonly IncidentViewModel _vm = new IncidentViewModel();

        public Archived()
        {
            InitializeComponent();
            SortDropdown.SelectedIndex = 0;
            Loaded += async (s, e) => await LoadIncidentsAsync();
        }

        private async Task LoadIncidentsAsync()
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

        // Opdaterer sorteringen når brugeren vælger i dropdown
        private void SortDropdown_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (SortDropdown.SelectedIndex == 0)
                _vm.SetSortStrategy(new SortbyDateNewest());
            else
                _vm.SetSortStrategy(new SortByDateOldest());

            _ = LoadIncidentsAsync();
        }

        private void IncidentList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (IncidentList.SelectedItem is IncidentReport selected)
            {
                // Hent den loggede bruger fra HomePageWindow
                User? loggedInUser = null;
                if (Window.GetWindow(this) is HomePageWindow__Shell_ shell)
                    loggedInUser = shell.LoggedInUser;

                NavigationService?.Navigate(new EditIncidentReport(selected, loggedInUser));
            }
        }
    }
}