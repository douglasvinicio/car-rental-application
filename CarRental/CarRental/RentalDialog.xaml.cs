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
        Customer currCustomer;
        Car currCar;

        public RentalDialog(Customer customer)
        {
            InitializeComponent();
            // Assigning Customer Object to currCustomer
            currCustomer = customer;
            lblCustomerName.Content = currCustomer.Name;
            FetchRecord();
        }


        // Choose an Existent client from a list
        private void btnChooseClient_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ClientsDialog clients = new ClientsDialog();
            clients.ShowDialog();

        }


        // Save Rental
        private void btnSaveRental_Click(object sender, RoutedEventArgs e)
        {
            currCar = (Car)cmbCars.SelectedItem;

            Rental rental = new Rental
            {
                CarId = currCar.CarId,
                CustomerId = currCustomer.CustomerId,
                RentalDate = dpRentalDate.SelectedDate.Value,
                ReturnDate = dpReturnDate.SelectedDate.Value
            };

            Global.context.Rentals.Add(rental);
            Global.context.SaveChanges();

            FetchRecord();
        }

        private void LvCarsOnRent_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

        }

        private void FetchRecord()
        {
            cmbCars.ItemsSource = Global.context.Cars.ToList();
            LvCarsOnRent.ItemsSource = Global.context.Rentals.Include("Customer").ToList();
            //lblNumOfCars.Content = LvCarsDialog.Items.Count;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FetchRecord();

        }
    }
}
