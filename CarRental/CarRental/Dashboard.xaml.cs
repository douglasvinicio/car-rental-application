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
    /// Interaction logic for Dashboard.xaml
    /// </summary>
    public partial class Dashboard : Window
    {
        public Dashboard()
        {
            InitializeComponent();
        }

        private void btnClients_Click(object sender, RoutedEventArgs e)
        {
            ClientsDialog clientsDialog = new ClientsDialog();
            clientsDialog.Owner = this;
            clientsDialog.ShowDialog();
        }

        private void btnCars_Click(object sender, RoutedEventArgs e)
        {
            CarDialog carDialog = new CarDialog();
            carDialog.Show();
        }

        private void btnRentals_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("Button Rentals was clicked!");
            RentalDialog rentalsDialog = new RentalDialog();
            rentalsDialog.ShowDialog();
        }
    }
}
