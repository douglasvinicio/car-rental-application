using System.Linq;
using System.Windows;


namespace CarRental
{
    public partial class Index : Window
    {     
        public Index()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            // Querying employee checking if login iformation matches with records on the database
            var employee = Global.context.Employees.Where(a => a.UserName == txtUsername.Text && a.Password == txtPassword.Password).SingleOrDefault();

            if (txtUsername.Text == "admin" && txtPassword.Password == "admin" || employee != null)
            {
                Dashboard dashboardWindow = new Dashboard();
                this.Close();
                dashboardWindow.ShowDialog();
            }
            else
            {
               MessageBox.Show("Either Username or password is incorrect.", "Login Error", MessageBoxButton.OK,MessageBoxImage.Warning, MessageBoxResult.No);
                return;
            }      
        }
    }
}
