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
            Dashboard dashboardWindow = new Dashboard();
            this.Close();
            dashboardWindow.ShowDialog();
        }
    }
}
