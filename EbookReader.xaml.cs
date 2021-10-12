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

namespace AWS_DynamoDB
{
    /// <summary>
    /// Interaction logic for EbookReader.xaml
    /// </summary>
    public partial class EbookReader : Window
    {
        public string userEmail {get; set;}
        public EbookReader(string email)
        {
            InitializeComponent();
            userEmail = email;

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblEmail.Content = userEmail;
        }
    }
}
