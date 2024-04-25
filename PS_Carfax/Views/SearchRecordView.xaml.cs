using PS_Carfax.UI.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace PS_Carfax.UI.Views
{
    /// <summary>
    /// Interaction logic for SearchRecordView.xaml
    /// </summary>
    public partial class SearchRecordView : Window
    {
        public SearchRecordView()
        {
            InitializeComponent();
            var app = (App)Application.Current;
            var dataService = app.DataService;
            DataContext = new SearchRecordViewModel(dataService);
           // btnAddFreeTextField.Click += btnAddFreeTextField_Click;
        }

        /*private void btnAddFreeTextField_Click(object sender, RoutedEventArgs e)
        {
            TextBox txtFreeText = new TextBox
            {
                Margin = new Thickness(0, 5, 0, 5)
            };
            spFreeText.Children.Add(txtFreeText);
        }*/
    }
}
