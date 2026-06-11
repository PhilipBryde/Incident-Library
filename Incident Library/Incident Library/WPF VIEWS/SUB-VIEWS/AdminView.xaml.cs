using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class AdminView : Page
    {
        private readonly AdminViewModel _vm = new AdminViewModel();

        public AdminView()
        {
            InitializeComponent();
        }

        // Indlæser brugerlisten når siden åbnes
        private async void Page_Loaded(object sender, RoutedEventArgs e)
        {
            var users = await _vm.GetAllUsersAsync();
            UserList.ItemsSource = users;
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
                // TODO: await _vm.RemoveUserAsync(selectedUser);
            }
        }

        private void BtnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            // TODO: open role picker dialog
        }
    }
}