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
    /// Interaction logic for ClientsDialog.xaml
    /// </summary>
    public partial class ClientsDialog : Window
    {
        private int id;
        private string Name;
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

        }

        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {

            CustomerDialog customerDialog = new CustomerDialog(id, Name, Address, City, State, Country, Phone, Email,Selected);
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
                this.Address = customer.Address;
                this.City = customer.City;
                this.State = customer.State;
                this.Country = customer.Country;
                this.Phone = customer.Phone;
                this.Email = customer.Email;
                this.Selected = true;           
            }

        }
    }
}
