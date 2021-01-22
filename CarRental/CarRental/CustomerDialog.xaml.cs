using System;
using System.Collections.Generic;
using System.Data.SqlClient;
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
    /// Interaction logic for CustomerDialog.xaml
    /// </summary>
    public partial class CustomerDialog : Window
    {
        
        List<Customer> customerList = new List<Customer>();
        public CustomerDialog(int id, string name,string driverLicenseNo, string address, string city, string state, string country, string phone,string email, bool selected)
        {
            InitializeComponent();

            
            if (selected == true)
            {
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                txtName.Text = name;
                txtLicenceNo.Text = driverLicenseNo;
                txtAddress.Text = address;
                txtCity.Text = city;
                txtState.Text = state;
                txtCountry.Text = country;
                txtPhone.Text = phone;
                txtEmail.Text = email;
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            MessageBox.Show("hello");

            if ((txtName.Text == "" || txtLicenceNo.Text==""|| txtAddress.Text==""|| txtCity.Text==""|| txtState.Text==""|| txtCountry.Text==""|| txtPhone.Text==""))
            {
                MessageBox.Show("Enter all the values", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            try
            {
                //insert into db
                CarsDatabaseContext ctx = new CarsDatabaseContext();
                Customer customer = new Customer() { Name = txtName.Text, DriverLicenseNo=txtLicenceNo.Text, Address = txtAddress.Text, City=txtCity.Text,State=txtState.Text,Country=txtCountry.Text,Phone=txtPhone.Text,Email=txtEmail.Text };
                ctx.Customers.Add(customer);
                ctx.SaveChanges();

                MessageBox.Show("record is added");
                customerList.Add(customer);
                ResetValue();
            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }
            
            ClientsDialog clientsDialog = new ClientsDialog();
            clientsDialog.Show();
        }
     //======================================================================================================
        private void ResetValue()
        {
            btnUpdate.IsEnabled = false;
            btnDelete.IsEnabled = false;
            txtName.Text = "";
            txtAddress.Text = "";
            txtCity.Text = "";
            txtState.Text = "";
            txtCountry.Text = "";
            txtPhone.Text = "";
            txtEmail.Text = "";
        }
        //======================================================================================================
    }
}
