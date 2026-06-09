using Incident_Library.MODELS__Data_;
using System.Windows;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS
{
    public partial class HomePageWindow__Shell_ : Window
    {
        private Button? _activeNavButton;

        public HomePageWindow__Shell_()
        {
            InitializeComponent();
            _activeNavButton = btnWorkInProgress;
            txtPageTitle.Text = "Work In Progress";
            ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.WorkInProgress());
        }

        // Navigation

        private void NavButton_Click(object sender, RoutedEventArgs e)
        {
            if (sender is not Button btn) return;

            string tag = btn.Tag?.ToString() ?? "";

            if (_activeNavButton != null)
                _activeNavButton.Style = (Style)FindResource("NavButton");

            btn.Style = (Style)FindResource("NavButtonActive");
            _activeNavButton = btn;

            string pageTitle = btn.Content?.ToString() ?? tag;
            txtPageTitle.Text = pageTitle;
            txtStatus.Text = $"Navigated to: {pageTitle}";

            switch (tag)
            {
                case "WorkInProgress":
                    ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.WorkInProgress());
                    break;
                case "UnderReview":
                    ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.UnderReview());
                    break;
                case "AwaitingApproval":
                    ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.AwaitingApproval());
                    break;
                case "Archived":
                    ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.Archived());
                    break;
                case "Admin":
                    ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.AdminView());
                    break;
            }
        }

            private void BtnNewIncident_Click(object sender, RoutedEventArgs e)
        {
            var newIncident = new IncidentReport();
            ContentArea.Navigate(new Incident_Library.WPF_VIEWS.SUB_VIEWS.EditIncidentReport(newIncident));
        }
        

        private void BtnEdit_Click(object sender, RoutedEventArgs e)
        {
            // TODO: pass selected incident to edit window
        }

        private void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to delete this incident?",
                "Confirm Delete",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: call IncidentRepository.DeleteAsync(selectedId)
                txtStatus.Text = "Incident deleted.";
            }
        }

        private void BtnSearch_Click(object sender, RoutedEventArgs e)
        {
            RunSearch();
        }

        private void TxtSearch_TextChanged(object sender, TextChangedEventArgs e)
        {
            
        }

        private void RunSearch()
        {
            string query = txtSearch.Text.Trim();
            if (string.IsNullOrEmpty(query)) return;
            txtStatus.Text = $"Searching for: \"{query}\"...";
            // TODO: call IncidentExplorerViewModel.SearchAsync(query)
        }

        private void BtnLogout_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show(
                "Are you sure you want to log out?",
                "Log Out",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                var login = new LoginWindow();
                login.Show();
                this.Close();
            }
        }

        // Helpers callable from sub-views 

        public void SetIncidentSelected(bool isSelected)
        {
            btnEdit.IsEnabled = isSelected;
            btnDelete.IsEnabled = isSelected;
        }

        public void SetIncidentCount(int count)
        {
            txtIncidentCount.Text = $"Incidents: {count}";
        }
    }
}