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
            Global.context = new CarsDatabaseContext();
            cmbFindAvailability.Items.Add("Available");
            cmbFindAvailability.Items.Add("Rented");
            cmbIsAvailable.Items.Add("Available");
            cmbIsAvailable.Items.Add("Rented");
            try
            {
                Global.context = new CarsDatabaseContext();

                FetchRecord();
                //lblNumOfCars.Content = LvCarsDialog.Items.Count;
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void FetchRecord()
        {
            // Include means force eager loading- used for collection in one-to-many relationship
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
            txtCatgory.Text = c.CarCategory;
            txtCapacity.Text = c.PassCapacity;
            txtAutoTransmission.Text = c.AutoTransmission;
            txtRentalFee.Text= c.RentalFee.ToString();
            txtBluetoothConn.Text = c.BluetoothConn;

            cmbIsAvailable.Text = c.IsAvailable.ToString();          
            currCarImage = c.Photo;
            //have a method to convert byte[] Bitmap
            imageViewer.Source = Utils.ByteArrayToBitmapImage(c.Photo);
            btnDelete.IsEnabled = true;
            btnUpdate.IsEnabled = true;
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
        private void BtnSave_Click(object sender, RoutedEventArgs e)     
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
                    CarCategory = txtCatgory.Text,
                    PassCapacity = txtCapacity.Text,
                    AutoTransmission = txtAutoTransmission.Text,
                    RentalFee=float.Parse(txtRentalFee.Text),
                    BluetoothConn=txtBluetoothConn.Text,
                    IsAvailable = cmbIsAvailable.Text,                   
                    Photo = currCarImage
                };

                Car car = Global.context.Cars.Add(c);
                Global.context.SaveChanges();

                ClearInputs();
                FetchRecord();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }
        private void btnDelete_Click(object sender, RoutedEventArgs e)
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
                ClearInputs();
                FetchRecord();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
        private void btnUpdate_Click(object sender, RoutedEventArgs e)
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
                carTobeUpdated.CarCategory = txtCatgory.Text;
                carTobeUpdated.PassCapacity = txtCapacity.Text;
                carTobeUpdated.AutoTransmission = txtAutoTransmission.Text;
                carTobeUpdated.RentalFee = float.Parse(txtRentalFee.Text);
                carTobeUpdated.BluetoothConn = txtBluetoothConn.Text;
                carTobeUpdated.IsAvailable = cmbIsAvailable.Text;
                carTobeUpdated.Photo = currCarImage;
              
                Global.context.SaveChanges();
                //ClearInputs();
                FetchRecord();
            }
            catch (SystemException ex)
            {
                MessageBox.Show(ex.Message, "Database operation failed", MessageBoxButton.OK, MessageBoxImage.Warning);
            }
        }
                    

        public bool IsFieldsValid()
        {
            if (txtRegNo.Text==""|| txtMake.Text == ""||txtModel.Text==""|| txtCarYear.Text==""|| txtCatgory.Text==""||
                 txtCapacity.Text==""|| txtAutoTransmission.Text==""|| txtRentalFee.Text==""|| txtBluetoothConn.Text==""||
                 cmbIsAvailable.Text=="")
            {
                MessageBox.Show("All fields must be filled", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            if (currCarImage == null)
            {
                MessageBox.Show("Choose a picture", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
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
            txtCatgory.Text = "";
            txtCapacity.Text = "";
            txtAutoTransmission.Text = "";
            txtRentalFee.Text = "";
            txtBluetoothConn.Text = "";
            cmbIsAvailable.Text = "";
            imageViewer.Source = null;
            btnDelete.IsEnabled = false;
            btnUpdate.IsEnabled = false;
            
            tbImage.Visibility = Visibility.Visible;
        }

       
        private void Window_Activated(object sender, EventArgs e)
        {
            FetchRecord();
        }

        private void cmbFindAvailability_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmbFindAvailability.SelectedItem.ToString() == "Available")
            {
                var carsAvailable = Global.context.Cars.Where(c => c.IsAvailable == "Available");
                LvCarsDialog.ItemsSource = carsAvailable.ToList();
            }
            else
            {
                var carsAvailable = Global.context.Cars.Where(c => c.IsAvailable == "Rented");
                LvCarsDialog.ItemsSource = carsAvailable.ToList();
            }
            lblNumOfCars.Content = LvCarsDialog.Items.Count;
        }
    }
}
