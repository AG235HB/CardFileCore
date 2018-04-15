using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CardFileCore.Models
{
    public class ApplicationContext:DbContext
    {
        public DbSet<Book> Books { get; set; }
        public DbSet<GivenBook> GivenBooks { get; set; }
        public DbSet<BookQuery> BookQueries { get; set; }
        public ApplicationContext(DbContextOptions<ApplicationContext> options) : base(options) { }
    }
}
