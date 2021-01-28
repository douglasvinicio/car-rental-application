using System.Windows;


namespace CarRental
{
    public partial class Index : Window
    {     
        public Index()
        {
            InitializeComponent();
            Global.context = new CarsDatabaseContext();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {

            if(Global.context.Employees.Find().UserName== txtUsername.Text && Global.context.Employees.Find().UserName == passwordbox.Password)
            {
                Dashboard dashboardWindow = new Dashboard();
                this.Close();
                dashboardWindow.ShowDialog();
            } else
            {
                MessageBox.Show("Either Username or password is incorrect.");
            }
        
        }
    }
}
