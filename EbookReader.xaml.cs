using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using Amazon.DynamoDBv2.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
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
using Expression = Amazon.DynamoDBv2.DocumentModel.Expression;

namespace AWS_DynamoDB
{
    /// <summary>
    /// Interaction logic for EbookReader.xaml
    /// </summary>
    public partial class EbookReader : Window
    {
        public string userEmail;
        public DDBOperation dynamoDb;
        public DynamoDBContext context;
        public static List<Bookshelf> shelfList;
        Amazon.DynamoDBv2.DocumentModel.Table bookShelfTable;
        private static List<Document> bookShelfSetQuery = new List<Document>();
        public static ObservableCollection<Bookshelf> myBookShelf = new ObservableCollection<Bookshelf>();
        public static string tableName = "Bookshelf";
        public EbookReader(string email, DDBOperation ddb)
        {
            InitializeComponent();
            userEmail = email;
            dynamoDb = ddb;
            context = new DynamoDBContext(ddb.ddbClient);
            bookShelfTable = Amazon.DynamoDBv2.DocumentModel.Table.LoadTable(dynamoDb.ddbClient, tableName, DynamoDBEntryConversion.V2);
            //var search = bookShelfTable.Query(email, new Expression());

        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            lblEmail.Content = userEmail;
            GetBooks();
        }


        public async Task GetBooks()
        {
            Search search = bookShelfTable.Query(userEmail,new Expression());
            try
            {
                do
                {
                    bookShelfSetQuery = await search.GetNextSetAsync();
                    foreach (var books in bookShelfSetQuery)
                    {
                        //Debug.WriteLine(books.ToJsonPretty());
                        //Debug.WriteLine(bookShelfSetQuery.ToJsonPretty());
                        Bookshelf newBookShelf = new Bookshelf(
                            books["Email"],
                            books["BookTitle"],
                            books["Url"],
                            books["Author"],
                            books["BookmarkedPage"],
                            books["BookmarkedTime"]);
                        //Debug.WriteLine($"{newBookShelf.Title}");
                        myBookShelf.Add(newBookShelf);
                        //Debug.WriteLine($"{myBookShelf[1].Title}");
                        //btn1Book.Content = myBookShelf[0].Title;

                    }
                } while (!search.IsDone);
            }
            catch (Exception e)
            {
                Debug.WriteLine($"{e.Message}");
            }

            btn1Book.Content = myBookShelf[0].Title + "\nAuthor: " + myBookShelf[0].Author;
            btn2Book.Content = myBookShelf[1].Title + "\nAuthor: " + myBookShelf[1].Author;
        }

        private void btn2Book_Click(object sender, RoutedEventArgs e)
        {
            string url = myBookShelf[1].Url;
            ViewBook window = new ViewBook(url);
            window.Show();
        }

        private void btn1Book_Click(object sender, RoutedEventArgs e)
        {
            string url = myBookShelf[0].Url;
            ViewBook window = new ViewBook(url);
            window.Show();
        }
    }

}
