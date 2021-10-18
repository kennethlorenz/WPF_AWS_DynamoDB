using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AWS_DynamoDB
{
    public class BookShelfItem
    {
        public string BookTitle { get; set; }
        public string Authors { get; set; }
        public string Key { get; set; }

        public BookShelfItem( string bookTitle, string authors, string key)
        {
            this.BookTitle = BookTitle;
            this.Authors = authors;
            this.Key = key;
        }
    }
}
