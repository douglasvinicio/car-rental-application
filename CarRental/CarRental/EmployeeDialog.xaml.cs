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
            if (lblId.Content.ToString()==null || txtEmployeeName.Text == "" || txtPassword.Password == ""|| txtSalary.Text==""|| txtRole.Text=="")
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
            lblId.Content = em.EmployeeID.ToString();
            txtEmployeeName.Text = em.UserName;
            txtPassword.Password = em.Password;
            txtSalary.Text = em.Salary;
            txtRole.Text = em.Role;
            btnClose.IsEnabled = true;
            btnUpdateEmployee.IsEnabled = true;
        }

        public void ClearInputs()
        {
            lblId.Content = "";
            txtEmployeeName.Text = "";
            txtPassword.Password = "";
            txtSalary.Text = "";
            txtRole.Text = "";
        }

        private void btnSaveEmployee_Click(object sender, RoutedEventArgs e)
        {
            //validation to check if the data is correct
            if (!IsFieldsValid()) { return; }
            try
            {
                Employee em = new Employee
                {
                    //EmployeeID = int.Parse(txtId.Text),
                    UserName = txtEmployeeName.Text,
                    Password = txtPassword.Password,
                    Salary= txtSalary.Text ,
                    Role= txtRole.Text
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