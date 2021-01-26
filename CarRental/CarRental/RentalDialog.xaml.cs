using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
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
        byte[] currCarImage;
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
            if (!IsFieldsValid()) { return; }
            try
            {
                currCar = (Car)cmbCars.SelectedItem;

                Rental rental = new Rental
                {
                    CarId = currCar.CarId,
                    CustomerId = currCustomer.CustomerId,
                    RentalDate = dpRentalDate.SelectedDate.Value,
                    ReturnDate = dpReturnDate.SelectedDate.Value,
                    TotalFee = float.Parse(lblTotalFess.Content.ToString())
                };

                Global.context.Rentals.Add(rental);
                Global.context.SaveChanges();
                ClearInputs();
                FetchRecord();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
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

        private void cmbCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            currCar = (Car)cmbCars.SelectedItem;
            lblRentalFee.Content = currCar.RentalFee;
            currCarImage = currCar.Photo;
            BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currCarImage); // ex: SystemException
            imageViewer.Source = bitmap;
            
        }

       
        private void returnDateSelectionChange(object sender, SelectionChangedEventArgs e)
        {

            currCar = (Car)cmbCars.SelectedItem;
            int NumOfDays;
            
            if (dpRentalDate.SelectedDate < dpReturnDate.SelectedDate)
            {
                NumOfDays = (dpReturnDate.SelectedDate - dpRentalDate.SelectedDate).Value.Days;
                lblTotalFess.Content = (NumOfDays * currCar.RentalFee).ToString();
            }
            else
            {              
                MessageBox.Show("The date selected must be more than starting date");
                dpReturnDate.SelectedDate = null;
                return;
            }
        }

        public bool IsFieldsValid()
        {
            if (lblCustomerName.Content.ToString() == "" || cmbCars.Text == "" || lblRentalFee.Content.ToString() == "" || dpRentalDate.Text == "" || dpReturnDate.Text == "" ||
                 lblTotalFess.Content.ToString()== "")
            {
                MessageBox.Show("All fields must be filled", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (currCarImage == null)
            {
                MessageBox.Show("Please choose a picture", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public void ClearInputs()
        {
            btnChooseClient.Content = "";
            lblCustomerName.Content = "";
            cmbCars.Text = "";
            lblRentalFee.Content = "";
            dpRentalDate.Text = "";
            dpReturnDate.Text = "";
            lblTotalFess.Content = "";
            btnUpdateRental.IsEnabled = false;
            btnClose.IsEnabled = false;
            imageViewer.Source = null;
        }

    }
}
