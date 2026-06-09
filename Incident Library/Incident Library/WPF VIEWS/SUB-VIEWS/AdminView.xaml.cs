using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class AdminView : Page
    {
        private readonly IncidentViewModel _vm = new IncidentViewModel();
        public AdminView()
        {
            InitializeComponent();
            // TODO: DataContext = new AdminViewModel();
            // await ViewModel.LoadUsersAsync();
        }

        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var all = await _vm.GetAllAsync();
            IncidentList.ItemsSource = all;
        }

        private void BtnEditIncident_Click(object sender, RoutedEventArgs e)
        {
            var btn = sender as Button;
            var incident = btn.Tag as IncidentReport;

            var editPage = new EditIncidentReport(incident);
            NavigationService.Navigate(editPage);
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            bool hasSelection = UserList.SelectedItem != null;
            btnRemoveUser.IsEnabled = hasSelection;
            btnChangeRole.IsEnabled = hasSelection;
        }

        private void BtnAddUser_Click(object sender, RoutedEventArgs e)
        {
            // TODO: open add user dialog
        }

        private void BtnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem == null) return;

            var result = MessageBox.Show(
                "Are you sure you want to remove this user?",
                "Confirm Remove",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // TODO: await ViewModel.RemoveUserAsync(selectedUserId);
            }
        }

        private void BtnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            // TODO: open role picker dialog
        }
    }
}