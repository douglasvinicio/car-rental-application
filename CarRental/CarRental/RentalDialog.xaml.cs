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

namespace CarRental
{
    /// <summary>
    /// Interaction logic for RentalDialog.xaml
    /// </summary>
    public partial class RentalDialog : Window
    {
        public RentalDialog()
        {
            InitializeComponent();
            //FetchRecord();
            
        }

        private void FetchRecord()
        {
            cmbCarReg.ItemsSource = Global.context.Cars.ToList();
            LvCarsOnRent.ItemsSource = Global.context.Cars.ToList();
            //lblNumOfCars.Content = LvCarsDialog.Items.Count;
        }

        private void LvCarsOnRent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void btnUpdateCar_Click(object sender, RoutedEventArgs e)
        {

        }
    }
}
