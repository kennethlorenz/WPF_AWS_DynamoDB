using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
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
        public string Key { get; set; }
        public string Author { get; set; }
        public DynamoDBEntry BookmarkedPage { get; set; }
        public string BookMarkedTime { get; set; }

        public static implicit operator Bookshelf(List<Document> v)
        {
            throw new NotImplementedException();
        }

        public Bookshelf(string email, string title, string key, string author, DynamoDBEntry bookmarkedPage, string bookmarkedTime)
        {
            this.Email = email;
            this.Title = title;
            this.Key = key;
            this.Author = author;
            this.BookmarkedPage = bookmarkedPage;
            this.BookMarkedTime = bookmarkedTime;
        }
    }
}
