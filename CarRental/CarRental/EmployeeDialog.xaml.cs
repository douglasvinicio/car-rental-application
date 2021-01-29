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
    public partial class EmployeeDialog : Window
    {
        Employee currEmployee;
        public EmployeeDialog()
        {
            InitializeComponent();
            FetchRecord();
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
                MessageBox.Show("Congratulations!\nEmployee added to the database with success!", "New Employee", MessageBoxButton.OK, MessageBoxImage.Information);
                Global.context.SaveChanges();

                ClearInputs();
                FetchRecord();
            }
            catch (SystemException exc)
            {
                MessageBox.Show(exc.Message);
            }
        }

        private void btnUpdateEmployee_Click(object sender, RoutedEventArgs e)
        {
            currEmployee.UserName = txtEmployeeName.Text;
            currEmployee.Password = txtPassword.Password;
            currEmployee.Salary = txtSalary.Text;
            currEmployee.Role = txtRole.Text;
            Global.context.SaveChanges();
            MessageBox.Show("Congratulations!\nEmployee updated with success!", "Update Employee", MessageBoxButton.OK, MessageBoxImage.Information);
            FetchRecord();
            ClearInputs();

        }

        private void btnDeleteEmployee_Click(object sender, RoutedEventArgs e)
        {
            if (MessageBox.Show("Delete this employee? \n Are you sure?", "Delete Employee", MessageBoxButton.YesNo, MessageBoxImage.Question) == MessageBoxResult.Yes)
            {
                Global.context.Employees.Remove(currEmployee);
                Global.context.SaveChanges();
                MessageBox.Show("Employee removed from database", "Confirmation Employee Delete", MessageBoxButton.OK, MessageBoxImage.Information);
                FetchRecord();
                ClearInputs();
            }
           
        }

        private void LvEmployees_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (lvEmployees.SelectedIndex == -1)
            {
                ClearInputs();
                return;
            }
            Employee em = (Employee)lvEmployees.SelectedItem;
            currEmployee = em;
            lblId.Content = em.EmployeeID.ToString();
            txtEmployeeName.Text = em.UserName;
            txtPassword.Password = em.Password;
            txtSalary.Text = em.Salary;
            txtRole.Text = em.Role;
            btnDeleteEmployee.IsEnabled = true;
            btnUpdateEmployee.IsEnabled = true;
        }

        private void Window_Activated(object sender, EventArgs e)
        {
            FetchRecord();
        }

        private void FetchRecord()
        {
            lvEmployees.ItemsSource = Global.context.Employees.ToList();
        }

        public bool IsFieldsValid()
        {
            if (lblId.Content.ToString() == null || txtEmployeeName.Text == "" || txtPassword.Password == "" || txtSalary.Text == "" || txtRole.Text == "")
            {
                MessageBox.Show("All fields must be filled", "Validation error", MessageBoxButton.OK, MessageBoxImage.Warning);
                return false;
            }
            return true;
        }

        public void ClearInputs()
        {
            lblId.Content = "";
            txtEmployeeName.Text = "";
            txtPassword.Password = "";
            txtSalary.Text = "";
            txtRole.Text = "";
        }
    }
}