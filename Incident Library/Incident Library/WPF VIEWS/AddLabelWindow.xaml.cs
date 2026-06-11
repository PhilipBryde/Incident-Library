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

namespace Incident_Library.WPF_VIEWS
{
    /// <summary>
    /// Interaction logic for AddLabelWindow.xaml
    /// </summary>
    public partial class AddLabelWindow : Window
    {
        public string LabelName { get; private set; } = "";

        public AddLabelWindow()
        {
            InitializeComponent();
            txtLabelName.Focus();
        }

        private void BtnOk_Click(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrWhiteSpace(txtLabelName.Text))
            {
                LabelName = txtLabelName.Text.Trim();
                DialogResult = true;
                Close();
            }
        }

        private void BtnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
            Close();
        }
    }
}
 
