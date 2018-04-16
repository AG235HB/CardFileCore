using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardFileCore.Models
{
    public class Book
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int Number { get; set; }
        public bool Availible { get; set; }
    }
}
