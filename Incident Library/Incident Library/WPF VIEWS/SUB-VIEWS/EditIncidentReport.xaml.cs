using Incident_Library.MODELS__Data_;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    public partial class EditIncidentReport : Page
    {
        public int? IncidentId { get; set; } = null;
        private readonly EditIncidentViewModel _vm;

        public EditIncidentReport(IncidentReport i)
        {
            InitializeComponent();
            _vm = new EditIncidentViewModel(i);

            txtTitle.Text = i.Title;
            txtHowDiscovered.Text = i.HowDiscovered;
            txtWhatIsIncident.Text = i.WhatIsIncident;
            txtHowResolved.Text = i.HowResolved;
        }

        public EditIncidentReport(int incidentId) : this()
        {
            IncidentId = incidentId;
            // TODO: load existing incident data into fields
            // var report = await ViewModel.GetIncidentAsync(incidentId);
            // txtTitle.Text = report.Title; etc.
        }

        public EditIncidentReport(EditIncidentReport? incident)
        {
        }

        public EditIncidentReport()
        {
        }

        private void BtnAddLabel_Click(object sender, RoutedEventArgs e)
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
                Text = "Label",
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

        private async void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            _vm.Incident.Title = txtTitle.Text;
            _vm.Incident.HowDiscovered = txtHowDiscovered.Text;
            _vm.Incident.WhatIsIncident = txtWhatIsIncident.Text;
            _vm.Incident.HowResolved = txtHowResolved.Text;
            _vm.Incident.Status = cmbStatus.SelectedIndex + 1;

            await _vm.SaveAsync();

            //if (string.IsNullOrWhiteSpace(txtTitle.Text))
            //{
            //    txtValidationError.Text = "Title is required.";
            //    txtValidationError.Visibility = Visibility.Visible;
            //    txtTitle.Focus();
            //    return;
            //}

            //txtValidationError.Visibility = Visibility.Collapsed;

            //try
            //{
            //    await _vm.SaveIncidentAsync(
            //        txtTitle.Text,
            //        txtHowDiscovered.Text,
            //        txtWhatIsIncident.Text,
            //        txtHowResolved.Text,
            //        1
            //        );
            //    MessageBox.Show("Incident Created");
                
            //}
            //catch
            //{
            //    MessageBox.Show("Incident Could not be saved");
            //}

            // TODO: build IncidentReport object and save
            // var report = new IncidentReport { Title = txtTitle.Text, ... };
            // await ViewModel.SaveAsync(report);

            // Navigate back
            NavigationService?.GoBack();
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