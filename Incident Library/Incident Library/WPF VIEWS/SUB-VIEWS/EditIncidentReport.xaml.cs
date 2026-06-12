using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;


namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class EditIncidentReport : Page
    {
        // Den loggede bruger - bruges til at styre hvilke knapper der vises
        private User? _loggedInUser;
        public int? IncidentId { get; set; } = null;
        private readonly EditIncidentViewModel _vm;

        public EditIncidentReport(IncidentReport i, User? loggedInUser = null)
        {
            InitializeComponent();
            _vm = new EditIncidentViewModel(i);
            _loggedInUser = loggedInUser;

            txtTitle.Text = i.Title;
            txtHowDiscovered.Text = i.HowDiscovered;
            txtWhatIsIncident.Text = i.WhatIsIncident;
            txtHowResolved.Text = i.HowResolved;
            cmbStatus.SelectedIndex = i.Status - 1;

            UpdateButtons(i.Status);

            // Vis de rigtige knapper baseret på status og brugerrolle
            UpdateButtons(i.Status);

            // Indlæs labels fra databasen når siden er klar
            Loaded += async (s, e) => await LoadLabelsAsync();

        }

        // Indlæser labels fra databasen og viser dem som badges
        private async Task LoadLabelsAsync()
        {
            await _vm.LoadLabelsAsync();

            foreach (var label in _vm.Incident.Labels)
            {
                AddLabelBadge(label.Name);
            }
        }

        // Tilføjer en label badge til panelet
        private void AddLabelBadge(string labelName)
        {
            var badge = new Border
            {
                Background = Brushes.LightGray,
                BorderBrush = Brushes.Gray,
                BorderThickness = new Thickness(1),
                Padding = new Thickness(6, 1, 6, 1),
                Margin = new Thickness(0, 0, 4, 0)
            };
            var panel = new StackPanel { Orientation = Orientation.Horizontal };
            panel.Children.Add(new TextBlock
            {
                Text = labelName,
                FontSize = 11,
                VerticalAlignment = VerticalAlignment.Center
            });
            var removeBtn = new Button
            {
                Content = "×",
                FontSize = 10,
                BorderThickness = new Thickness(0),
                Background = Brushes.Transparent,
                Cursor = System.Windows.Input.Cursors.Hand,
                Margin = new Thickness(4, 0, 0, 0),
                Padding = new Thickness(0)
            };
            removeBtn.Click += (s, ev) => labelsPanel.Children.Remove(badge);
            panel.Children.Add(removeBtn);
            badge.Child = panel;

            int insertIndex = labelsPanel.Children.Count - 1;
            labelsPanel.Children.Insert(insertIndex, badge);
        }



        // Styrer hvilke knapper der vises baseret på status og om brugeren er admin
        private void UpdateButtons(int status)
        {
            bool isAdmin = _loggedInUser?.Role == 1;

            // Skjul alle send-knapper først
            btnSendToReview.Visibility = Visibility.Collapsed;
            btnSendToApproval.Visibility = Visibility.Collapsed;
            btnApprove.Visibility = Visibility.Collapsed;
            btnDecline.Visibility = Visibility.Collapsed;

            // Status 1 = Work In Progress
            if (status == 1)
            {
                btnSendToReview.Visibility = Visibility.Visible;
            }
            // Status 2 = Under Review
            else if (status == 2)
            {
                btnSendToApproval.Visibility = Visibility.Visible;
            }
            // Status 3 = Awaiting Approval - kun admin kan se knapperne
            else if (status == 3 && isAdmin)
            {
                btnApprove.Visibility = Visibility.Visible;
                btnDecline.Visibility = Visibility.Visible;
            }
            // Status 3 = Awaiting Approval - almindelige brugere kan kun læse og knappers visning bliver fjernet
            else if (status == 3 && !isAdmin)
            {
                btnSave.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                txtTitle.IsReadOnly = true;
                txtWhatIsIncident.IsReadOnly = true;
                txtHowDiscovered.IsReadOnly = true;
                txtHowResolved.IsReadOnly = true;
                cmbStatus.IsEnabled = false;
            }
            // Status 4 = Archived - kun admin kan redigere
            else if (status == 4 && !isAdmin)
            {
                // Skjul Save, Delete og gør felter readonly for almindelige brugere
                btnSave.Visibility = Visibility.Collapsed;
                btnDelete.Visibility = Visibility.Collapsed;
                txtTitle.IsReadOnly = true;
                txtWhatIsIncident.IsReadOnly = true;
                txtHowDiscovered.IsReadOnly = true;
                txtHowResolved.IsReadOnly = true;
                cmbStatus.IsEnabled = false;
            }
        }

        public EditIncidentReport()
        {
        }


        private void BtnAddLabel_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new AddLabelWindow();
            dialog.Owner = Window.GetWindow(this);
            bool? result = dialog.ShowDialog();

            if (result == true)
            {
                AddLabelBadge(dialog.LabelName);
            }
        }

        // Sender incident videre til Under Review
        private async void BtnSendToReview_Click(object sender, RoutedEventArgs e)
        {
            _vm.Incident.Status = 2;
            await _vm.SaveAsync();
            NavigationService?.GoBack();
        }

        // Sender incident videre til Awaiting Approval - Rasmus
        private async void BtnSendToApproval_Click(object sender, RoutedEventArgs e)
        {
            //MessageBox.Show($"Nuværende status: {_vm.Incident.Status}");
            _vm.Incident.Status = 3;
            await _vm.SaveAsync();
            NavigationService?.GoBack();
        }

        // Godkender incident og sender til Archived - Rasmus
        private async void BtnApprove_Click(object sender, RoutedEventArgs e)
        {
            _vm.Incident.Status = 4;
            await _vm.SaveAsync();
            NavigationService?.GoBack();
        }

        // Afviser incident og sender tilbage til Work In Progress - Rasmus
        private async void BtnDecline_Click(object sender, RoutedEventArgs e)
        {
            _vm.Incident.Status = 1;
            await _vm.SaveAsync();
            NavigationService?.GoBack();
        }

        private async void BtnSave_Click(object sender, RoutedEventArgs e) //Save Knap; sender brugers input videre til ViewModel - Rasmus
        {
            _vm.Incident.Title = txtTitle.Text;
            _vm.Incident.HowDiscovered = txtHowDiscovered.Text;
            _vm.Incident.WhatIsIncident = txtWhatIsIncident.Text;
            _vm.Incident.HowResolved = txtHowResolved.Text;
            _vm.Incident.Status = cmbStatus.SelectedIndex + 1;

            // Saml labels fra panelet og gem dem på incident objektet - Rasmus
            _vm.Incident.Labels.Clear();
            foreach (var child in labelsPanel.Children)
            {
                if (child is Border badge && badge.Child is StackPanel panel)
                {
                    if (panel.Children[0] is TextBlock txt)
                    {
                        _vm.Incident.Labels.Add(new Incident_Library.MODELS__Data_.Label // _vm.Incident.Labels.Add(new Label = synes åbenbart det var dårligt skrevet
                        {
                            Name = txt.Text,
                            IncidentId = _vm.Incident.Id
                        });
                    }
                }
            }

            await _vm.SaveAsync();
            
            NavigationService?.GoBack(); //Går tilbage til den tidligere page
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            NavigationService?.GoBack();
        }

        private async void BtnDelete_Click(object sender, RoutedEventArgs e)
        {
            var warning = MessageBox.Show("Are you sure you want to delete this incident?", "Confirm", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if(warning == MessageBoxResult.Yes)
            {
                await _vm.DeleteAsync();
                NavigationService.GoBack();
            }
        }
    }
}