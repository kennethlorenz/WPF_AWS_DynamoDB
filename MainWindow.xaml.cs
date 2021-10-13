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

namespace AWS_DynamoDB
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DDBOperation ddb = new DDBOperation();

        public MainWindow()
        {
            InitializeComponent();
            //ddb.CreateTable();
            //ddb.InsertLoginCredentials();
        }

        private void btnLogin_Click(object sender, RoutedEventArgs e)
        {
            GetUser();
        }

        private async void GetUser()
        {
            string email = txtBxEmail.Text;
            string password = pwBxPassword.Password.ToString();
            await ddb.GetUser(email, password);

            if (ddb.userExists == true)
            {
                this.Hide();
                EbookReader window = new EbookReader(email, ddb);
                window.Show();
            }
            else
            {
                lblValidation.Content = "Invalid Email / Password.";
            }
        }


    }
}
