using Amazon.Runtime;
using Amazon.S3;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Lab1_AwsS3_IAM_Service
{
    public static class Helper
    {
        public static AmazonS3Client s3Client = GetAmazonS3Client();

        private static AmazonS3Client GetAmazonS3Client() {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("AppSettings.json");

            var accessKeyID = builder.Build().GetSection("AWSCredentials").GetSection("AccesskeyID").Value;
            var secretKey = builder.Build().GetSection("AWSCredentials").GetSection("Secretaccesskey").Value;

            BasicAWSCredentials credentials = new BasicAWSCredentials(accessKeyID, secretKey);
            s3Client = new AmazonS3Client(credentials, Amazon.RegionEndpoint.USEast1);

            return s3Client;
        }
    }
}
