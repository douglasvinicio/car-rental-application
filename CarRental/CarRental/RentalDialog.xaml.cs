using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media.Imaging;


namespace CarRental
{
    public partial class RentalDialog : Window
    {
        DateTime thisDay = DateTime.Today;
        Customer currCustomer;
        byte[] currCarImage;
        Car currCar;
        int currNumOfDays;

        public RentalDialog(Customer customer)
        {
            InitializeComponent();
            // Assigning Customer Object to currCustomer
            currCustomer = customer;
            if (currCustomer != null)
            {
                lblCustomerName.Content = currCustomer.Name;
            }
            FetchRecord();
        }

        // Choose an existent client from a list button
        private void btnChooseClient_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
            ClientsDialog clients = new ClientsDialog();
            clients.ShowDialog();
        }

        // Save Rental button
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

                if (MessageBox.Show("Create this Rental Order? !", "Rental Order", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
                {

                    Global.context.Rentals.Add(rental);
                    Global.context.SaveChanges();

                    // Setting Car.IsAvailable to false
                    CarAvailable(rental, false);
                    MessageBox.Show("Rental created with success !", "Rental Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                    ClearInputs();
                }
                else
                {
                    return;
                }
                FetchRecord();


            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        // Update Rental Order
        private void btnUpdateRental_Click(object sender, RoutedEventArgs e)
        {
            if (tabAllRentals.IsSelected)
            {
                Rental rentalUpdate = (Rental)lvAllRentals.SelectedItem;

                // Making previous car available and new selected not available if car is changed
                if (rentalUpdate.CarId != currCar.CarId)
                {
                    CarAvailable(rentalUpdate, true);
                    rentalUpdate.CarId = currCar.CarId;
                    currCar.IsAvailable = false;
                }
                rentalUpdate.CustomerId = currCustomer.CustomerId;
                rentalUpdate.RentalDate = dpRentalDate.SelectedDate.Value.Date;
                rentalUpdate.ReturnDate = dpReturnDate.SelectedDate.Value.Date;
                rentalUpdate.TotalFee = float.Parse(lblTotalFess.Content.ToString());
                rentalUpdate.TotalDays = currNumOfDays;
                rentalUpdate.Status = Rental.StatusEnum.Rented;
                rentalUpdate.Comments = txtComments.Text;

                CarAvailable(rentalUpdate, false);

                Global.context.SaveChanges();

                MessageBox.Show("Update with Success!", "Update Rental Order", MessageBoxButton.OK, MessageBoxImage.Information);
                FetchRecord();
            }
        }

        // Finalize Order Button
        private void btnFinalizeOrder_Click(object sender, RoutedEventArgs e)
        {
            if (tabAllRentals.IsSelected == true)
            {
                Rental rentedFinalize = (Rental)lvAllRentals.SelectedItem;
                rentedFinalize.Status = Rental.StatusEnum.Finalized;
                CarAvailable(rentedFinalize, true);
            }
            if (tabReturned.IsSelected == true)
            {
                Rental returnFinalized = (Rental)lvReturned.SelectedItem;
                returnFinalized.Status = Rental.StatusEnum.Finalized;
                CarAvailable(returnFinalized, true);
            }
            if (tabRented.IsSelected == true)
            {
                Rental returnFinalized = (Rental)lvRented.SelectedItem;
                returnFinalized.Status = Rental.StatusEnum.Finalized;
                CarAvailable(returnFinalized, true);
            }


            MessageBox.Show("Rental order closed", "Done", MessageBoxButton.OK, MessageBoxImage.Information);
        }

        // On combobox changed change picture of the car
        private void cmbCars_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbCars.SelectedItem != null)
            {
                currCar = (Car)cmbCars.SelectedItem;
                lblRentalFee.Content = currCar.RentalFee;
                currCarImage = currCar.Photo;
                BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currCarImage);
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

        // Method to make car available or not availbale passing Rental and Bool as parameters
        private void CarAvailable(Rental rental, bool available)
        {
            //Making Car.IsAvailable field false if rented.
            Car result = (from c in Global.context.Cars
                          where c.CarId == rental.CarId
                          select c).SingleOrDefault();

            result.IsAvailable = available;

            Global.context.SaveChanges();
        }

        private void FetchRecord()
        {

            //Populating All Rentals 
            lvAllRentals.ItemsSource = Global.context.Rentals.ToList();

            //Populating Rented
            lvRented.ItemsSource = Global.context.Rentals.Where(a => a.Status == Rental.StatusEnum.Rented).ToList();

            //Populating Returned
            lvReturned.ItemsSource = Global.context.Rentals.Where(a => a.ReturnDate < thisDay || a.Status == Rental.StatusEnum.Finalized).ToList();

            // Showing only available to rent cars on ComboBox
            cmbCars.ItemsSource = Global.context.Cars.Where(a => a.IsAvailable == true).ToList();

            //Populating Labels 
            lblNumOfCars.Content = Global.context.Cars.Count();
            lblNumOfCarsOnRent.Content = Global.context.Cars.Where(a => a.IsAvailable == false).Count();
            lblNumOfCarsAvailable.Content = Global.context.Cars.Where(a => a.IsAvailable == true).Count();

            // Setting Blackout Dates to 14 days or 2 weeks on the past max.
            CalendarDateRange cdr = new CalendarDateRange(DateTime.MinValue, DateTime.Today.AddDays(-14));
            dpRentalDate.BlackoutDates.Add(cdr);
            dpReturnDate.BlackoutDates.Add(cdr);
        }


        private void lvAllRentals_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rental rental = (Rental)lvRented.SelectedItem;
            if (rental != null)
            {
                PopulateFieldsOnListChanged(rental);
            }
        }

        private void lvRented_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rental rental = (Rental)lvRented.SelectedItem;
            if (rental != null)
            {
                PopulateFieldsOnListChanged(rental);
            }
        }

        private void lvReturned_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Rental rental = (Rental)lvRented.SelectedItem;
            if (rental != null)
            {
                PopulateFieldsOnListChanged(rental);
            }
        }

        // Populating fields / Method is being used in 3 different List Views
        private void PopulateFieldsOnListChanged(Rental rental)
        {
            btnUpdateRental.IsEnabled = true;
            currCar = rental.Car;
            currCustomer = rental.Customer;
            cmbCars.SelectedItem = "";
            lblCustomerName.Content = currCustomer.Name;
            lblNumOfDays.Content = rental.TotalDays;
            currCarImage = currCar.Photo;
            BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currCarImage);
            imageViewer.Source = bitmap;
            dpRentalDate.SelectedDate = rental.RentalDate;
            dpReturnDate.SelectedDate = rental.ReturnDate;
            txtComments.Text = rental.Comments;
        }
        private void Window_Activated(object sender, EventArgs e)
        {
            // When clients dialog is closed fetch data from db again
            FetchRecord();
        }
        public bool IsFieldsValid()
        {
            if (lblCustomerName.Content.ToString() == "" || cmbCars.Text == "" || lblRentalFee.Content.ToString() == "" || dpRentalDate.Text == "" || dpReturnDate.Text == "" ||
                 lblTotalFess.Content.ToString() == "")
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
            lblCustomerName.Content = "";
            cmbCars.SelectedItem = "";
            lblRentalFee.Content = "";
            lblTotalFess.Content = "";
            lblNumOfDays.Content = "";
            imageViewer.Source = null;
        }
    }
}
