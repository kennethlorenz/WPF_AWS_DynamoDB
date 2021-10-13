using Amazon.S3.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_DynamoDB
{
    public class ViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged(PropertyChangedEventArgs e)
        {
            if (PropertyChanged != null)
            {
                PropertyChanged(this, e);
            }
        }

        private MemoryStream docStream;

        public MemoryStream DocumentStream
        {
            get { return docStream; }
            set
            {
                docStream = value;
                OnPropertyChanged(new PropertyChangedEventArgs("DocumentStream"));
            }
        }

        public ViewModel(string url)
        {
            GetObjectRequest request = new GetObjectRequest();
            request.BucketName = "lab2comp306bucket";
            request.Key = url;
            GetObjectResponse response = S3Operations.s3Client.GetObjectAsync(request).Result;

            response.ResponseStream.CopyTo(docStream);

        }

}
}
