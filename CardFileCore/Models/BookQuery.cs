using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardFileCore.Models
{
    public class BookQuery
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string BookName { get; set; }
        public DateTime Validity { get; set; }
    }
}
