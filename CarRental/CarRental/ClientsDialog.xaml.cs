﻿using System;
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
    
    public partial class ClientsDialog : Window
    {
        public Customer newCustomer;

        public ClientsDialog()
        {
            InitializeComponent();
            LoadData();            
        }

        // Buttons
        private void btnAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            CustomerDialog customerDialog = new CustomerDialog(newCustomer);
            customerDialog.ShowDialog();
        }

        private void btnEditCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customerUpdate = (Customer)LvClientDialog.SelectedItem;
            CustomerDialog customerDialog = new CustomerDialog(customerUpdate);
            customerDialog.ShowDialog();
        }

        private void btnDeleteCustomer_Click(object sender, RoutedEventArgs e)
        {
            Customer customerDelete = (Customer)LvClientDialog.SelectedItem;
            Global.context.Customers.Remove(customerDelete);
            Global.context.SaveChanges();

            LoadData();
        }
        private void btnClose_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        // Selected Item
        private void LvClientDialog_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            btnEditCustomer.IsEnabled = true;
            btnDeleteCustomer.IsEnabled = true;
            
        }

        private void LoadData()
        {
            List<Customer> customerList = Global.context.Customers.ToList<Customer>();
            LvClientDialog.ItemsSource = customerList;
            LvClientDialog.Items.Refresh();
        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }
        private void Window_Activated(object sender, EventArgs e)
        {
            LoadData();
        }

    }
}
