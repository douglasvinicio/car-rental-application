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
   
    public partial class ReturnDialog : Window
    {
        Customer currCustomer;
        byte[] currCarImage;
        Rental currRental;
        Car currCar;
        public ReturnDialog()
        {
            InitializeComponent();
            FetchRecord();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValid()) { return; }
            try
            {
                currRental = (Rental)cmbCarId.SelectedItem;
                int  NumOfDays = (dpReturnDate.SelectedDate - currRental.RentalDate).Value.Days;
                float fine = 50 * NumOfDays;
                //save the values in Rental table        
                Returns returns = new Returns
                {
                    CarId = currRental.CarId,
                    CustomerId = currRental.CustomerId,
                    RentalDate = currRental.RentalDate,
                    ReturnDate = dpReturnDate.SelectedDate.Value,
                    Fine = fine
                };
                //Global.context.Returns.Add(returns);
                Global.context.SaveChanges();

                //now make the status of that car in Cars class to false
                Car result = (from c in Global.context.Cars
                              where c.CarId == currCar.CarId
                              select c).SingleOrDefault();
                result.IsAvailable = true;
                Global.context.SaveChanges();
                //clear the inputs and fetch the records again   
                FetchRecord();
                MessageBox.Show("Record Added");
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
            //ClearInputs();
        }

        public bool IsFieldsValid()
        {
            if (cmbCarId.Text == "" || txtRegNo.Text == "" || txtCustomerId.Text == "" || lblRentalDate.Content.ToString() == ""|| dpReturnDate.Text == "" || lblFine.Content.ToString() == "" )
            {
                MessageBox.Show("All fields must be filled", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
           
            return true;
        }

        public void ClearInputs()
        {
            txtRegNo.Text = "";
            cmbCarId.Text = "";
            txtRegNo.Text = "";
            txtCustomerId.Text = "";
            lblRentalDate.Content = "";
            dpReturnDate.Text = "";
            lblFine.Content = "";        
        }


        private void FetchRecord()
        {
            //we should get the carIds of only cars which are present in rental table
            cmbCarId.ItemsSource = Global.context.Cars.Include("Rental").ToList();
            LvCarsOnRent.ItemsSource = Global.context.Rentals.Include("Customer").ToList();
            //lblNumOfCars.Content = LvCarsDialog.Items.Count;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FetchRecord();
        }

    }
}
