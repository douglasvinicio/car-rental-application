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
    /// Interaction logic for CustomerDialog.xaml
    /// </summary>
    public partial class CustomerDialog : Window
    {
        public CustomerDialog(int id, string name, string address, string city, string state, string country, string phone,string email, bool selected)
        {
            InitializeComponent();
            
            if (selected == true)
            {
                btnUpdate.IsEnabled = true;
                btnDelete.IsEnabled = true;
                txtName.Text = name;
                txtAddress.Text = address;
                txtCity.Text = city;
                txtState.Text = state;
                txtCountry.Text = country;
                txtPhone.Text = phone;
                txtEmail.Text = email;
            }
        }
    }
}
