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
    /// Interaction logic for CarDialog.xaml
    /// </summary>
    public partial class CarDialog : Window
    {
        byte[] currCarImage;

        public CarDialog()
        {
            InitializeComponent();
            FetchRecord();
        }
        private void FetchRecord()
        {
            LvCarsDialog.ItemsSource = Global.context.Cars.ToList();
            lblNumOfCars.Content = LvCarsDialog.Items.Count;
        }


        private void LvCarsDialog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (LvCarsDialog.SelectedIndex == -1)
            {
                ClearInputs();
                return;
            }
            Car c = (Car)LvCarsDialog.SelectedItem;
            txtRegNo.Text = c.RegNum;
            txtMake.Text = c.Make;
            txtModel.Text = c.Model;
            txtCarYear.Text = c.CarYear;
            cmbCategory.Text = c.CarCategory;
            cmbCapacity.Text = c.PassengerCapacity.ToString();
            chboxAutomatic.IsChecked = c.AutoTransmission;
            txtRentalFee.Text= c.RentalFee.ToString();
            chboxBluetooth.IsChecked= c.BluetoothConn;
            chboxAvailable.IsChecked = c.IsAvailable;
            currCarImage = c.Photo;
            imageViewer.Source = Utils.ByteArrayToBitmapImage(c.Photo);
            btnDeleteCar.IsEnabled = true;
            btnUpdateCar.IsEnabled = true;
        }

        private void BtnAddCar_Click(object sender, RoutedEventArgs e)     
        {
            //validation to check if the data is correct
            if (!IsFieldsValid()) { return; }
            try
            {
                Car c = new Car
                {
                    RegNum = txtRegNo.Text,
                    Make = txtMake.Text,
                    Model = txtModel.Text,
                    CarYear = txtCarYear.Text,
                    CarCategory = cmbCategory.Text,
                    PassengerCapacity = int.Parse(cmbCapacity.Text),
                    RentalFee = float.Parse(txtRentalFee.Text),
                    AutoTransmission = chboxAutomatic.IsChecked.Value,
                    BluetoothConn = chboxBluetooth.IsChecked.Value,
                    IsAvailable = chboxAvailable.IsChecked.Value,
                    Photo = currCarImage
                };
                // Adding new car to database and saving it
                Car car = Global.context.Cars.Add(c);
                Global.context.SaveChanges();

                MessageBox.Show("Congratulations!\nCar added to the database!", "New Car", MessageBoxButton.OK, MessageBoxImage.Information);

                ClearInputs();
                FetchRecord();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        // Update Car Method
        private void btnUpdateCar_Click(object sender, RoutedEventArgs e)
        {
            if (!IsFieldsValid()) { return; }

            Car carTobeUpdated = (Car)LvCarsDialog.SelectedItem;

            if (carTobeUpdated == null) { return; }

            try
            {
                carTobeUpdated.RegNum = txtRegNo.Text;
                carTobeUpdated.Make = txtMake.Text;
                carTobeUpdated.Model = txtModel.Text;
                carTobeUpdated.CarYear = txtCarYear.Text;
                carTobeUpdated.CarCategory = cmbCategory.Text;
                carTobeUpdated.PassengerCapacity = int.Parse(cmbCapacity.Text);
                carTobeUpdated.AutoTransmission = chboxAutomatic.IsChecked.Value;
                carTobeUpdated.RentalFee = float.Parse(txtRentalFee.Text);
                carTobeUpdated.BluetoothConn = chboxBluetooth.IsChecked.Value;
                carTobeUpdated.IsAvailable = chboxAvailable.IsChecked.Value;
                carTobeUpdated.Photo = currCarImage;
              
                Global.context.SaveChanges();

                MessageBox.Show("Congratulations!\nCar information updated with success!", "Car Update", MessageBoxButton.OK, MessageBoxImage.Information);
                FetchRecord();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnDeleteCar_Click(object sender, RoutedEventArgs e)
        {
            Car carTobeDeleted = (Car)LvCarsDialog.SelectedItem;

            if (carTobeDeleted == null) { return; }
            if (MessageBoxResult.Yes != MessageBox.Show("Do you want to delete the record?\n" + carTobeDeleted,
                "Warning", MessageBoxButton.YesNo, MessageBoxImage.Warning))
            { return; }
            try
            {
                Global.context.Cars.Remove(carTobeDeleted);
                Global.context.SaveChanges();
                MessageBox.Show("Car removed from the database","Car Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                ClearInputs();
                FetchRecord();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }

        private void btnImage_Click(object sender, RoutedEventArgs e)
        {
            OpenFileDialog dlg = new OpenFileDialog();
            dlg.Filter = "Image files (*.jpg;*.jpeg;*.gif;*.png)|*.jpg;*.jpeg;*.gif;*.png|All Files (*.*)|*.*";
            dlg.RestoreDirectory = true;

            if (dlg.ShowDialog() == true)
            {
                try
                {
                    currCarImage = File.ReadAllBytes(dlg.FileName);
                    tbImage.Visibility = Visibility.Hidden;
                    BitmapImage bitmap = Utils.ByteArrayToBitmapImage(currCarImage); // ex: SystemException
                    imageViewer.Source = bitmap;
                }
                catch (Exception ex) when (ex is SystemException || ex is IOException)
                {
                    MessageBox.Show(ex.Message, "File reading failed", MessageBoxButton.OK, MessageBoxImage.Warning);
                }
            }
        }

        public bool IsFieldsValid()
        {
            if (txtRegNo.Text == "" || txtMake.Text == "" || txtModel.Text == "" || txtCarYear.Text == "" || cmbCategory.Text == "" ||
                 cmbCapacity.Text == "" || txtRentalFee.Text == "" )
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
            txtRegNo.Text = "";
            txtMake.Text = "";
            txtModel.Text = "";
            txtCarYear.Text = "";
            cmbCategory.Text = "";
            cmbCapacity.Text = "";
            chboxAutomatic.IsChecked = false;
            txtRentalFee.Text = "";
            chboxBluetooth.IsChecked = false;
            chboxAvailable.IsChecked = false;
            imageViewer.Source = null;
            btnDeleteCar.IsEnabled = false;
            btnUpdateCar.IsEnabled = false;
            tbImage.Visibility = Visibility.Visible;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FetchRecord();
        }
    }
}