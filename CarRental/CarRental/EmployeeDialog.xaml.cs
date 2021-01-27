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
    /// Interaction logic for EmployeeDialog.xaml
    /// </summary>
    public partial class EmployeeDialog : Window
    {
        public EmployeeDialog()
        {
            InitializeComponent();
            FetchRecord();
        }

        private void FetchRecord()
        {
            lvEmployees.ItemsSource = Global.context.Employees.ToList();
            //lblNumOfCars.Content = lvEmployees.Items.Count;
        }

        public bool IsFieldsValid()
        {
            if (txtId.Text == "" || txtEmployeeName.Text == "" || txtPassword.Text == "")
            {
                MessageBox.Show("All fields must be filled", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        private void LvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvEmployees.SelectedIndex == -1)
            {
                ClearInputs();
                return;
            }
            Employee em = (Employee)lvEmployees.SelectedItem;
            txtId.Text = em.EmployeeID.ToString();
            txtEmployeeName.Text = em.UserName;
            txtPassword.Text = em.Password;

            btnClose.IsEnabled = true;
            btnUpdateEmployee.IsEnabled = true;
        }

        public void ClearInputs()
        {
            txtId.Text = "";
            txtEmployeeName.Text = "";
            txtPassword.Text = "";
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            //validation to check if the data is correct
            if (!IsFieldsValid()) { return; }
            try
            {
                Employee em = new Employee
                {
                    EmployeeID = int.Parse(txtId.Text),
                    UserName = txtEmployeeName.Text,
                    Password = txtPassword.Text,
                };

                Employee employee = Global.context.Employees.Add(em);
                Global.context.SaveChanges();

                ClearInputs();
                FetchRecord();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FetchRecord();
        }
    }
}