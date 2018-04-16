using Microsoft.EntityFrameworkCore;

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
