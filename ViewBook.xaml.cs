using Amazon.DynamoDBv2.DataModel;
using Amazon.S3.Model;
using Syncfusion.Windows.Shared;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
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
    /// Interaction logic for ViewBook.xaml
    /// </summary>
    public partial class ViewBook : Window
    {
        string bucketName = "lab2comp306bucket";
        DynamoDBContext context;
        Bookshelf book;
        int bookmarkedPage;
        public ViewBook(Bookshelf bookshelfItem, DynamoDBContext context)
        {
            InitializeComponent();
            this.book = bookshelfItem;
            this.context = context;

            //load the book based on the book's bucketname and key
            GetObjectRequest req = new GetObjectRequest();
            req.BucketName = bucketName;
            req.Key = book.Key;
            GetObjectResponse response = S3Operations.s3Client.GetObjectAsync(req).Result;
            MemoryStream docStream = new MemoryStream();
            response.ResponseStream.CopyTo(docStream);
            pdfViewer.ItemSource = docStream;
            pdfViewer.GotoPage(bookshelfItem.BookmarkedPage);
        }

        public async Task BookMark()
        {
            CancellationToken cancellationToken = default;
            bookmarkedPage = pdfViewer.CurrentPageIndex;
            try
            {
                //Create and load the book from DynamoDB
                Bookshelf bookRetrieved = await context.LoadAsync<Bookshelf>(book.Email, book.Title, cancellationToken);
                //upddate the bookmarked page & date
                bookRetrieved.BookmarkedPage = bookmarkedPage;
                bookRetrieved.BookmarkedTime = DateTime.Now.ToString();
                //save it
                context.SaveAsync(bookRetrieved);
                Debug.WriteLine($"Book: {book.Title} \nBookmarktime: {bookRetrieved.BookmarkedTime}, \nBookmarkedPage: {bookmarkedPage}");
            }

            catch(Exception e)
            {
                Debug.WriteLine(e.Message);
            }

        }

        private void btnBookmark_Click(object sender, RoutedEventArgs e)
        {
            //call the bookmark method when the bookmark button is clicked
            BookMark();
        }

    }
}
