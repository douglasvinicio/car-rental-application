using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace CarRental
{
    public partial class RentalDialog : Window
    {
        Customer currCustomer;
        byte[] currCarImage;
        Car currCar;
        int currNumOfDays;

        public RentalDialog(Customer customer)
        {
            InitializeComponent();
            // Assigning Customer Object to currCustomer
            currCustomer = customer;
            lblCustomerName.Content = currCustomer.Name;
            FetchRecord();
        }

        // Choose an existent client from a list
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
                //Assigning car selected on Combobox to currCar
                currCar = (Car)cmbCars.SelectedItem;

                //Saving rental order
                Rental rental = new Rental
                {
                    CarId = currCar.CarId,
                    CustomerId = currCustomer.CustomerId,
                    RentalDate = dpRentalDate.SelectedDate.Value.Date,
                    ReturnDate = dpReturnDate.SelectedDate.Value.Date,
                    TotalFee = float.Parse(lblTotalFess.Content.ToString()),
                    TotalDays = currNumOfDays,
                    Status = Rental.StatusEnum.Rented,
                    Comments = txtComments.Text
                };

                Global.context.Rentals.Add(rental);
                Global.context.SaveChanges();

                // Setting Car.IsAvailable to false
                CarAvailable(rental, false);

                MessageBox.Show("Rental order created with success!");

                //Clear the inputs and fetch the records again   
                FetchRecord();

            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnFinalizeOrder_Click(object sender, RoutedEventArgs e)
        {
            if (tabRented.IsSelected == true)
            {
                Rental rentedFinalize = (Rental)lvRented.SelectedItem;
                rentedFinalize.Status = Rental.StatusEnum.Finalized;
                CarAvailable(rentedFinalize, true);
            }
            if (tabReturned.IsSelected == true)
            {
                Rental returnFinalized = (Rental)lvReturned.SelectedItem;
                returnFinalized.Status = Rental.StatusEnum.Finalized;
                CarAvailable(returnFinalized, true);
            }

            MessageBox.Show("Rental order created with success!");
        }

        private void cmbCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCars.SelectedItem != null)
            {
                currCar = (Car)cmbCars.SelectedItem;
                lblRentalFee.Content = currCar.RentalFee;
                currCarImage = currCar.Photo;
                BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currCarImage); // ex: SystemException
                imageViewer.Source = bitmap;
            }
            else
            {
                return;
            }
        }

        // Calculating days and total fees on date change
        private void returnDateSelectionChange(object sender, SelectionChangedEventArgs e)
        {

            currCar = (Car)cmbCars.SelectedItem;
            
            // Checking if RentalDate is lower then ReturnDate
            if (dpRentalDate.SelectedDate < dpReturnDate.SelectedDate)
            {
                currNumOfDays = (dpReturnDate.SelectedDate - dpRentalDate.SelectedDate).Value.Days;
                lblTotalFess.Content = (currNumOfDays * currCar.RentalFee).ToString();
                lblNumOfDays.Content = currNumOfDays;
            }
            else
            {              
                MessageBox.Show("The date selected must be more than starting date");
                dpReturnDate.SelectedDate = null;
                return;
            }
        }

        private void CarAvailable(Rental rental, bool available)
        {
            //Making Car.IsAvailable field false if rented.
            Car result = (from c in Global.context.Cars
                          where c.CarId == rental.CarId
                          select c).SingleOrDefault();

            result.IsAvailable = available;

            Global.context.SaveChanges();
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

        private void FetchRecord()
        {
            //Todays date
            DateTime thisDay = DateTime.Today;

            //Populating All Rentals 
            lvAllRentals.ItemsSource = Global.context.Rentals.ToList();

            //Populating Rented
            lvRented.ItemsSource = Global.context.Rentals.Where(a => a.Status == Rental.StatusEnum.Finalized).ToList();

            //Populating Returned
            lvReturned.ItemsSource = Global.context.Rentals.Where(a => a.ReturnDate < thisDay || a.Status == Rental.StatusEnum.Finalized).ToList();

            // Showing only available to rent cars on ComboBox
            cmbCars.ItemsSource = Global.context.Cars.Where(a => a.IsAvailable == true).ToList();

            //Populating Labels 
            lblNumOfCars.Content = Global.context.Cars.Count();
            lblNumOfCarsOnRent.Content = Global.context.Cars.Where(a => a.IsAvailable == false).Count();
            lblNumOfCarsAvailable.Content = Global.context.Cars.Where(a => a.IsAvailable == true).Count();
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            // When clients is closed fetch data from db again
            FetchRecord();
        }

        public void ClearInputs()
        {
            btnChooseClient.Content = "";
            lblCustomerName.Content = "";
            cmbCars.SelectedItem = "";
            lblRentalFee.Content = "";
            dpRentalDate.SelectedDate = null;
            dpReturnDate.SelectedDate = null;
            lblTotalFess.Content = "";
            imageViewer.Source = null;
        }
    }
}
