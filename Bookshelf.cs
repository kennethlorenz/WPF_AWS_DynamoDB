using Amazon.DynamoDBv2.DataModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_DynamoDB
{
    [DynamoDBTable("Bookshelf")]
    public class Bookshelf
    {
        [DynamoDBHashKey]
        public string Email { get; set; }

        public string Title { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public int BookmarkedPage { get; set; }
        public string BookMarkTime { get; set; }
    }
}
