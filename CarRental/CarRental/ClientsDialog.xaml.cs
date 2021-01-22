using System;
using System.Collections.Generic;
using System.Data.Entity;
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
    /// Interaction logic for ClientsDialog.xaml
    /// </summary>
    public partial class ClientsDialog : Window
    {
        //creating DAL(data access layer)
        const string connstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Sim_IPD-program\course contents\12..NET_VB_C#\ToDoWithDatabase\ToDoWithDatabases\ToDoDatabase.mdf;Integrated Security=True;Connect Timeout=30";

        private int id;
        private string Name;
        private string DriverLicenseNo;
        private string Address;
        private string City;
        private string State;
        private string Country;
        private string Phone;
        private string Email;
        bool Selected=false;
       
        public ClientsDialog()
        {
            InitializeComponent();
            Global.context = new CarsDatabaseContext();
            SqlConnection conn = new SqlConnection(connstring);
            conn.Open();
        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            CustomerDialog customerDialog = new CustomerDialog(id, Name, DriverLicenseNo, Address, City, State, Country, Phone, Email,Selected);
            customerDialog.Owner = this;
            customerDialog.ShowDialog();
        }

        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void LvClientDialog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnAddCustomer.Content = "Edit/Delete";
            var selectedItem = LvClientDialog.SelectedItem;
            if (selectedItem is Customer)
            {
                Customer customer = (Customer)LvClientDialog.SelectedItem;
                this.id = customer.CustomerId;
                this.Name = customer.Name;
                this.DriverLicenseNo = customer.DriverLicenseNo;
                this.Address = customer.Address;
                this.City = customer.City;
                this.State = customer.State;
                this.Country = customer.Country;
                this.Phone = customer.Phone;
                this.Email = customer.Email;
                this.Selected = true;           
            }

        }

        private void LoadFile()
        {
            CarsDatabaseContext ctx = new CarsDatabaseContext();

            //fetching all data with LINQ
            List<Customer> customer1 = (from c in ctx.Customers select c).ToList<Customer>();
            List<Customer> customer2 = ctx.Customers.ToList<Customer>();
            //LvClientDialog.ItemsSource = Global.context.Customers.Include().ToList();
        }
    }
}
