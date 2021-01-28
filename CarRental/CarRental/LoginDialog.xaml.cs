using System.Linq;
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

            //if(Global.context.Employees.Find().UserName== txtUsername.Text && Global.context.Employees.Find().Password == passwordbox.Password.ToString())
            //{
            var queryEmployee = from em in Global.context.Employees.ToList()
                                where em.UserName == txtUsername.Text && em.Password== passwordbox1.Password
                                select em;
            if (queryEmployee != null)
            {
                Dashboard dashboardWindow = new Dashboard();
                this.Close();
                dashboardWindow.ShowDialog();
            }
            else
            {
               MessageBox.Show("Either Username or password is incorrect.");
            }      
        }
    }
}
