using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Controls;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{ //Rasmus
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

        private async void BtnRemoveUser_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is not User selectedUser) return;

            var result = MessageBox.Show(
                $"Are you sure you want to remove {selectedUser.Name}?",
                "Confirm Remove",
                MessageBoxButton.YesNo,
                MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                // Sletter brugeren fra databasen
                await _vm.DeleteUserAsync(selectedUser);

                // Genindlæser brugerlisten så den slettede bruger forsvinder
                var users = await _vm.GetAllUsersAsync();
                UserList.ItemsSource = users;
            }
        }

        private async void BtnChangeRole_Click(object sender, RoutedEventArgs e)
        {
            if (UserList.SelectedItem is not User selectedUser) return;

            // Skifter rollen og opdaterer databasen
            await _vm.ToggleRoleAsync(selectedUser);

            // Genindlæser listen så den nye rolle vises
            var users = await _vm.GetAllUsersAsync();
            UserList.ItemsSource = users;
        }
    }
}