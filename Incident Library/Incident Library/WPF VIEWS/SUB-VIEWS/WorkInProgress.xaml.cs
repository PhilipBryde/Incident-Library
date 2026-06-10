using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Incident_Library.SORTING;
using Incident_Library.Repository;
using Incident_Library.VIEWMODELS_LOGIC_;
using System.Diagnostics.Eventing.Reader;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    /// <summary>
    /// Interaction logic for WorkInProgress.xaml
    /// </summary>
    public partial class WorkInProgress : Page 
    {
        private IncidentViewModel _ViewModel = new IncidentViewModel();
        public WorkInProgress()
        {
            InitializeComponent();
            SortDropdown.SelectedIndex = 0;
            LoadIncidentsAsync();
            // TODO: DataContext = new IncidentExplorerViewModel();
            // await ViewModel.LoadIncidentsByStatusAsync(1); // 1 = Work In Progress
        }

        private async Task LoadIncidentsAsync()
        {
            var viewModel = new IncidentViewModel();
            var incidents = await viewModel.GetByStatusAsync(1);

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
                _ViewModel.SetSortStrategy(new SortbyDateNewest());
            else
                _ViewModel.SetSortStrategy(new SortByDateOldest());

            LoadIncidentsAsync();
        }
    }
}
