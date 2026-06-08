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
using Incident_Library.Repository;
using Incident_Library.VIEWMODELS_LOGIC_;

namespace Incident_Library.WPF_VIEWS.SUB_VIEWS
{
    /// <summary>
    /// Interaction logic for WorkInProgress.xaml
    /// </summary>
    public partial class WorkInProgress : Page 
    {
        public WorkInProgress()
        {
            InitializeComponent();
            LoadIncidentsAsync();
            // TODO: DataContext = new IncidentExplorerViewModel();
            // await ViewModel.LoadIncidentsByStatusAsync(1); // 1 = Work In Progress
        }

        private async Task LoadIncidentsAsync()
        {
            var viewModel = new IncidentExplorerViewModel();
            var incidents = await viewModel.GetByStatusAsync(1);

            if (incidents.Count == 0)
            {
                txtEmpty.Visibility = System.Windows.Visibility.Visible;
            }
            else
            {
                IncidentList.ItemsSource = incidents;
            }
        }
    }
}
