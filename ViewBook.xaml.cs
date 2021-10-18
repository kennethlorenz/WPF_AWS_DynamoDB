using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.IO;
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
    /// Interaction logic for ViewBook.xaml
    /// </summary>
    public partial class ViewBook : Window
    {
        string bucketName = "lab2comp306bucket";
        public ViewBook(string key)
        {
            InitializeComponent();
            GetObjectRequest req = new GetObjectRequest();
            req.BucketName = bucketName;
            req.Key = key;
            GetObjectResponse response = S3Operations.s3Client.GetObjectAsync(req).Result;

            MemoryStream docStream = new MemoryStream();
            response.ResponseStream.CopyTo(docStream);

            pdfViewer.ItemSource = docStream;
        }

        
    }
}
