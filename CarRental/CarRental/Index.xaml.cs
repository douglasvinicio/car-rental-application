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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace CarRental
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class Index : Window
    {
        //creating DAL(data access layer)
        const string connstring = @"Data Source=(LocalDB)\MSSQLLocalDB;AttachDbFilename=C:\Sim_IPD-program\course contents\12..NET_VB_C#\ToDoWithDatabase\ToDoWithDatabases\ToDoDatabase.mdf;Integrated Security=True;Connect Timeout=30";

        public Index()
        {
            InitializeComponent();
            Global.context = new CarsDatabaseContext();
            //SqlConnection conn = new SqlConnection(connstring);
            //conn.Open();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            Dashboard dashboardWindow = new Dashboard();
            this.Close();
            dashboardWindow.ShowDialog();
        }
    }
}
