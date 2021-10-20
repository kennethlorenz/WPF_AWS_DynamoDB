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
        DDBOperation ddb = new DDBOperation("Bookshelf");
        public DynamoDBContext context;
        Amazon.DynamoDBv2.DocumentModel.Table bookShelfTable;
        private static List<Document> bookShelfSetQuery = new List<Document>();
        public static ObservableCollection<Bookshelf> myBookShelf = new ObservableCollection<Bookshelf>();
        public static string tableName = "Bookshelf";
        public EbookReader(string email)
        {
            InitializeComponent();

            //store users email locally
            userEmail = email;
            //connect table using dynamodbcontext
            context = new DynamoDBContext(ddb.ddbClient);
            
            //load table into our local Table
            bookShelfTable = Amazon.DynamoDBv2.DocumentModel.Table.LoadTable(ddb.ddbClient, tableName, DynamoDBEntryConversion.V2);
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            //display users email
            lblEmail.Content = userEmail;

            GetBooks();
        }

        //this method goes through the local table that we have.
        public async Task GetBooks()
        {
            //perform a search on the local table based on the users email.
            Search search = bookShelfTable.Query(userEmail,new Expression());
            try
            {
                do
                {   //store the books the user has into the List
                    bookShelfSetQuery = await search.GetNextSetAsync();

                    //loop through the list,
                    //for each book on the list, create a bookshelf item and store it to a collection
                    foreach (var book in bookShelfSetQuery)
                    {
                        //Debug.WriteLine(books.ToJsonPretty());
                        //Debug.WriteLine(bookShelfSetQuery.ToJsonPretty());
                        Bookshelf newBookShelf = new Bookshelf(
                            book["Email"],
                            book["BookTitle"],
                            book["Key"],
                            book["Author"],
                            (int)book["BookmarkedPage"],
                            book["BookmarkedTime"]);
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

            //loop through the collection and display it in the Listbox
            foreach (Bookshelf book in myBookShelf)
            {
                lstBxBooks.Items.Add(new ListBoxItem() { Content = book.Title + " Author: " + book.Author,
                    //we use the tag property of the listbox item to store the books key
                    //the key will be used to create a bookshelf item that will be passed (Line 111)
                    //to the viewbook window (Line 116)
                    Tag = book.Key});
            }
        }


        private void btnViewBook_Click(object sender, RoutedEventArgs e)
        {

            string key = ((ListBoxItem)lstBxBooks.SelectedItem).Tag.ToString();
            //Create a bookshelf item based on the book selected in the listbox
            //using its key
            Bookshelf bShelf = myBookShelf.FirstOrDefault(b => b.Key == key);
            Debug.WriteLine(key + " " + bShelf.Title);

            //use the bookshelf we just created to the viewbook window
            //along with the dynamodbcontext
            ViewBook window = new ViewBook(bShelf, context);
            window.Show();
        }
    }

}
