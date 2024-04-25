using PS_Carfax.Data.Models;
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
    /// Interaction logic for ShowRecordResultView.xaml
    /// </summary>
    public partial class ShowRecordResultView : Window
    {
        public ShowRecordResultView()
        {
            InitializeComponent();
        }

        private void lvHistory_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (sender is ListView listView && listView.SelectedItem is History selectedHistory)
            {
                ((ShowRecordResultViewModel)DataContext).SelectedHistory = selectedHistory;
            }
        }
    }

}
