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
    public partial class CustomerDialog : Window
    {
        ClientsDialog clientsDialog = new ClientsDialog();
        public Customer currCustomer; 
        public CustomerDialog(Customer customer)
        {
            InitializeComponent();

            if (customer != null)
            {
                currCustomer = customer;
                PopulateFields();
            }
        }

        private void BtnSave_Click(object sender, RoutedEventArgs e)
        {
            if ((txtName.Text == "" || txtLicenceNo.Text==""|| txtAddress.Text==""|| txtCity.Text==""|| txtState.Text==""|| txtCountry.Text==""|| txtPhone.Text==""))
            {
                MessageBox.Show("Please enter all the values", "Error", MessageBoxButton.OK, MessageBoxImage.Error);
                return;
            }
            
            try
            {    // If customer exists, update the data            
                if (currCustomer != null)
                {
                    currCustomer.Name = txtName.Text;
                    Global.context.SaveChanges();
                    MessageBox.Show("Congratulations!\nCustomer Updated with success!", "Customer Update" , MessageBoxButton.OK, MessageBoxImage.Information);
                    this.Close();

                }
                // If customer doesn't exist yet create a new one 
                else
                {
                    Customer customer = new Customer() { Name = txtName.Text, DriverLicenseNo = txtLicenceNo.Text, Address = txtAddress.Text, City = txtCity.Text, State = txtState.Text, Country = txtCountry.Text, Phone = txtPhone.Text, Email = txtEmail.Text };
                    Global.context.Customers.Add(customer);
                    MessageBox.Show("Congratulations!\nCustomer added to the database!", "New Customer", MessageBoxButton.OK, MessageBoxImage.Information);
                    Global.context.SaveChanges();
                    this.Close();
                }

            }
            catch (SqlException exception)
            {
                Console.WriteLine(exception.Message);
            }

            
        }

        private void PopulateFields()
        {
            txtName.Text = currCustomer.Name;
            txtLicenceNo.Text = currCustomer.DriverLicenseNo;
            txtAddress.Text = currCustomer.Address;
            txtCity.Text = currCustomer.City;
            txtState.Text = currCustomer.State;
            txtCountry.Text = currCustomer.Country;
            txtPhone.Text = currCustomer.Phone;
            txtEmail.Text = currCustomer.Email;
        }

    }



}
