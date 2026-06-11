using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Input;

namespace Incident_Library.WPF_VIEWS
{
    public partial class LoginWindow : Window
    {
        private readonly LoginViewModel _vm = new LoginViewModel();

        public LoginWindow()
        {
            InitializeComponent();
        }

        private void BtnLogin_Click(object sender, RoutedEventArgs e)
        {
            AttemptLogin();
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }

        private void PasswordBox_KeyDown(object sender, KeyEventArgs e)
        {
            // Tillader brugeren at trykke Enter i stedet for at klikke Log In
            if (e.Key == Key.Enter)
                AttemptLogin();
        }

        private async void AttemptLogin()
        {
            string name = txtUsername.Text.Trim();
            string password = pwdPassword.Password;

            // Tjek at felterne ikke er tomme
            if (string.IsNullOrEmpty(name) || string.IsNullOrEmpty(password))
            {
                txtError.Text = "Please enter both username and password.";
                txtError.Visibility = Visibility.Visible;
                return;
            }

            // Spørg ViewModel om login er korrekt mod databasen
            bool success = await _vm.LoginAsync(name, password);

            if (success)
            {
                // Login lykkedes - åbn hovedvinduet og send den loggede bruger med
                var home = new HomePageWindow__Shell_(_vm.LoggedInUser);
                home.Show();
                this.Close();
            }
            else
            {
                // Forkert brugernavn eller kodeord
                txtError.Text = "Invalid username or password.";
                txtError.Visibility = Visibility.Visible;
                pwdPassword.Clear();
                pwdPassword.Focus();
            }
        }
    }
}